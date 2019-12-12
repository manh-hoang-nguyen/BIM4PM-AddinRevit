namespace ProjectManagement.Tools.Project
{
    using Autodesk.Revit.DB;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using ProjectManagement.Tools.History;
    using ProjectManagement.Tools.Synchronize;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ProjectViewModel : ViewModelBase
    {
       
        public RelayCommand<ProjectView> WindowLoaded { get; set; }

        public RelayCommand<UserControl> SendData { get; set; }

        public RelayCommand<ProjectView> Connect { get; set; }

        public RelayCommand<ProjectView> Disconnect { get; set; }

        public RelayCommand<ProjectView> ModelSelection { get; set; }

        public RelayCommand<ProjectView> ProjectSelection { get; set; }

        public RelayCommand<ProjectView> Compare { get; set; }

        public RelayCommand<ProjectView> Synchronize { get; set; }

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
        private bool btnSynchronizeIsEnable;
        private bool btnConnectIsEnable;

        public bool BtnConnectIsEnable
        {
            get => btnConnectIsEnable; set { btnConnectIsEnable = value; RaisePropertyChanged(); }
        }

        private bool btnDisconnectIsEnable;

        public bool BtnDisconnectIsEnable
        {
            get => btnDisconnectIsEnable; set { btnDisconnectIsEnable = value; RaisePropertyChanged(); }
        }

        public bool BtnSynchronizeIsEnable { get => btnSynchronizeIsEnable; set { btnSynchronizeIsEnable = value; RaisePropertyChanged(); } }

        public ProjectViewModel()
        {

            CbProjectIsEnable = true;
            CbModelIsEnable = true;
            Model = new ProjectModel();
            WindowLoaded = new RelayCommand<ProjectView>(OnWindowLoaded);

            ProjectSelection = new RelayCommand<ProjectView>(OnProjectSelection);
            ModelSelection = new RelayCommand<ProjectView>(OnModelSelection);
            Connect = new RelayCommand<ProjectView>(OnConnect);
            Disconnect = new RelayCommand<ProjectView>(OnDisconnect);
            SendData = new RelayCommand<UserControl>(OnSendData);
            Compare = new RelayCommand<ProjectView>(OnCompare);
            Synchronize = new RelayCommand<ProjectView>(OnSynchronize);
        }

        private void OnSynchronize(ProjectView obj)
        {
            SyncView syncView = new SyncView() { DataContext = new SyncViewModel() };
            syncView.ShowDialog();
        }

        private void OnCompare(ProjectView view)
        {
            if (ModelProvider.Instance.DicRevitElements == null || ProjectProvider.Instance.DicRevitElements == null) {
                MessageBox.Show("You have to connect first!");
                return;
            } 
            else CompareProvider.Instance.Execute();

            if (CompareProvider.Instance.IsUpToDate()) {
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
        /// The when select project, set current project
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnModelSelection(ProjectView view)
        {
            if (view.Projects.SelectedItem != null)
            {
                BtnConnectIsEnable = true;
             
            }
          if(view.Models.SelectedItem != null)
            {
                ModelProvider.Instance.CurrentModel = view.Models.SelectedItem as Document;
            }
          
        }

        /// <summary>
        /// Get data from database and model
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnConnect(ProjectView view)
        {
            BtnConnectIsEnable = false;
            if (ProjectProvider.Instance.CurrentVersion == null) return;

            Thread thread = new Thread(() =>
            {
                ProjectProvider.Instance.DicRevitElements = new Dictionary<string, RevitElement>();
                foreach (RevitElement e in Model.GetRevitElementInCloud(ProjectProvider.Instance.CurrentVersion))
                {
                    ProjectProvider.Instance.DicRevitElements.Add(e.guid, e);
                }

                BtnDisconnectIsEnable = true;
                CbModelIsEnable = false;
                CbProjectIsEnable = false;
                BtnSynchronizeIsEnable = true;
            });
            thread.Start();

            Model.GetParamterElement();

            AuthProvider.Instance.Connect();

          
        }

        /// <summary>
        /// Disconnect to current project
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private void OnDisconnect(ProjectView view)
        {
            AuthProvider.Instance.Disconnect();
            Versions = null;
            BtnDisconnectIsEnable = false;
            BtnConnectIsEnable = true;
            CbProjectIsEnable = true;
            CbModelIsEnable = true;
            BtnSynchronizeIsEnable = false;
            view.Projects.SelectedItem = null;
            view.Models.SelectedItem = null;
            view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.White);
            
        }

        /// <summary>
        /// TWhen load window, load user and user's projects too
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private async void OnWindowLoaded(ProjectView view)
        {
            if(AuthProvider.Instance.IsConnected == false)
            {
                Task<User> userTask = Model.GetUser();
                Task<List<ProjectManagement.Models.Project>> projects = Model.GetUserProjectsAsync();
                view.Models.ItemsSource = ModelProvider.Instance.Models;
                User = await userTask;
                Projects = await projects;

                BtnDisconnectIsEnable = false;
                BtnSynchronizeIsEnable = false;
                BtnConnectIsEnable = false;
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
        /// When select project, set provider of current project and current version of project too
        /// </summary>
        /// <param name="view">The view<see cref="ProjectView"/></param>
        private   void OnProjectSelection(ProjectView view)
        {

            if (view.Projects.SelectedItem != null)
            {
                Project selectedProject = view.Projects.SelectedItem as Project;
                ProjectProvider.Instance.CurrentProject = selectedProject;
                 
                Versions = ProjectProvider.Instance.Versions;
                 
                if (view.Models.SelectedItem != null) BtnConnectIsEnable = true;
            }
            else
            {
                ProjectProvider.Instance.CurrentVersion = null;
                BtnConnectIsEnable = false;
            }
        }
    }
}
