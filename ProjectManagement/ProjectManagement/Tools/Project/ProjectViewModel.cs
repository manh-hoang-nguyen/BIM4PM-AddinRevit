namespace ProjectManagement.Tools.Project
{
    using Autodesk.Revit.DB;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using ProjectManagement.Tools.Synchronize;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ProjectViewModel : ViewModelBase
    {
        public RelayCommand<ProjectView> WindowLoaded { get; set; }

        public RelayCommand<UserControl> SendData { get; set; }

        public RelayCommand<ProjectView> ConnectCommand { get; set; }

        public RelayCommand<ProjectView> DisconnectCommand { get; set; }

        public RelayCommand<ProjectView> ModelSelectionCommand { get; set; }

        public RelayCommand<ProjectView> ProjectSelectionCommand { get; set; }

        public RelayCommand<ProjectView> CompareCommand { get; set; }

        public RelayCommand<ProjectView> SynchronizeCommand { get; set; }

        public ProjectModel Model { get; set; }

        public User User
        {
            get => _user; set { _user = value; RaisePropertyChanged(); }
        }

        public List<Models.Project> Projects
        {
            get => _projects; set { _projects = value; RaisePropertyChanged(); }
        }

        private List<Models.Project> _projects;

        public List<Models.Version> Versions
        {
            get => _versions; set { _versions = value; RaisePropertyChanged(); }
        }

        private List<Models.Version> _versions;

        private User _user;

        private bool cbProjectIsEnable;

        public bool CbProjectIsEnable
        {
            get => cbProjectIsEnable; set { cbProjectIsEnable = value; RaisePropertyChanged(); }
        }

        private bool cbModelIsEnable;

        public bool CbModelIsEnable
        {
            get => cbModelIsEnable; set { cbModelIsEnable = value; RaisePropertyChanged(); }
        }

        public ProjectViewModel()
        {
            CbProjectIsEnable = true;
            CbModelIsEnable = true;
            Model = new ProjectModel();
            WindowLoaded = new RelayCommand<ProjectView>(OnWindowLoaded);

            ProjectSelectionCommand = new RelayCommand<ProjectView>(OnProjectSelection);
            ModelSelectionCommand = new RelayCommand<ProjectView>(OnModelSelection);
            ConnectCommand = new RelayCommand<ProjectView>(OnConnect, CanConnect);
            DisconnectCommand = new RelayCommand<ProjectView>(OnDisconnect, CanDisconnect);
            SendData = new RelayCommand<UserControl>(OnSendData);
            CompareCommand = new RelayCommand<ProjectView>(OnCompare, CanDisconnect);
            SynchronizeCommand = new RelayCommand<ProjectView>(OnSynchronize, CanDisconnect);
        }

        private bool CanDisconnect(ProjectView arg)
        {
            if (AuthProvider.Instance.IsConnected == true) return true;
            return false;
        }

        private bool CanConnect(ProjectView arg)
        {

            if (ModelProvider.Instance.CurrentModel != null
                && ProjectProvider.Instance.CurrentProject != null
                && AuthProvider.Instance.IsConnected == false) return true;
            return false;
        }

        private void OnSynchronize(ProjectView obj)
        {
            SyncView syncView = new SyncView() { DataContext = new SyncViewModel() };
            syncView.ShowDialog();
        }

        private void OnCompare(ProjectView view)
        {
            if (ModelProvider.Instance.DicRevitElements == null || ProjectProvider.Instance.DicRevitElements == null)
            {
                MessageBox.Show("You have to connect first!");
                return;
            }
            else CompareProvider.Instance.Execute();

            if (CompareProvider.Instance.IsUpToDate())
            {
                MessageBox.Show("Your model is up to date");
                view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.LightGreen);
            }
            else
            {
                MessageBox.Show("Your model is not up to date");
                view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.Red);
            }
        }

        /// <summary>
        /// Get data from database and model
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnConnect(ProjectView view)
        {
            if (ProjectProvider.Instance.CurrentVersion == null) return;

            Thread thread = new Thread(() =>
            {
                ProjectProvider.Instance.DicRevitElements = new Dictionary<string, RevitElement>();
                foreach (RevitElement e in Model.GetRevitElementInCloud(ProjectProvider.Instance.CurrentVersion))
                {
                    ProjectProvider.Instance.DicRevitElements.Add(e.guid, e);
                }

                CbModelIsEnable = false;
                CbProjectIsEnable = false;
            });
            thread.Start();

            Model.GetParamterElement();

            AuthProvider.Instance.IsConnected = true;
            ConnectCommand.RaiseCanExecuteChanged();
            DisconnectCommand.RaiseCanExecuteChanged();
            SynchronizeCommand.RaiseCanExecuteChanged();
            CompareCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Disconnect to current project
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnDisconnect(ProjectView view)
        {
            AuthProvider.Instance.IsConnected = false;

            Versions = null;

            CbProjectIsEnable = true;
            CbModelIsEnable = true;

            view.Projects.SelectedItem = null;
            view.Models.SelectedItem = null;
            view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.White);

            ConnectCommand.RaiseCanExecuteChanged();
            DisconnectCommand.RaiseCanExecuteChanged();
            SynchronizeCommand.RaiseCanExecuteChanged();
            CompareCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// TWhen load window, load user and user's projects too
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private async void OnWindowLoaded(ProjectView view)
        {
            if (AuthProvider.Instance.IsConnected == false)
            {
                Task<User> userTask = Model.GetUser();
                Task<List<Project>> projects = Model.GetUserProjectsAsync();
                view.Models.ItemsSource = ModelProvider.Instance.Models;
                User = await userTask;
                Projects = await projects;

                CbProjectIsEnable = true;
                CbModelIsEnable = true;
                view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.White);
            }
        }

        /// <summary>
        /// The OnSendData
        /// </summary>
        /// <param name="win">The win<see cref="UserControl"/></param>
        private void OnSendData(UserControl win)
        {
            Model.SendRevitElementToCloud();
        }

        /// <summary>
        /// The when select project, set current project
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnModelSelection(ProjectView view)
        {

            if (view.Models.SelectedItem != null)
            {
                ModelProvider.Instance.CurrentModel = view.Models.SelectedItem as Document;
                ConnectCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// When select project, set provider of current project and current version of project too
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnProjectSelection(ProjectView view)
        {

            if (view.Projects.SelectedItem != null)
            {
                Project selectedProject = view.Projects.SelectedItem as Project;
                ProjectProvider.Instance.CurrentProject = selectedProject;

                Versions = ProjectProvider.Instance.Versions;
                 
                ConnectCommand.RaiseCanExecuteChanged();
            }
            else
            {
                ProjectProvider.Instance.CurrentVersion = null;
            }
        }
    }
}
