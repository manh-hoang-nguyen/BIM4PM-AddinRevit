namespace ProjectManagement.Tools.Project
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using ProjectManagement.Utils;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using ProjectManagement.Utils.RevitUtils;

    public class ProjectViewModel : ViewModelBase
    {
        private UIApplication _uiapp;

        public ProjectModel Model { get; set; }

        public RelayCommand<Window> WindowLoaded { get; set; }
        public RelayCommand<Window> SendData { get; set; }
        public User User
        {
            get => _user; set { _user = value; RaisePropertyChanged(); }
        }

        private DocumentSet documents;

        public DocumentSet Documents
        {
            get => documents; set { documents = value; RaisePropertyChanged(); }
        }

        public List<Models.Project> Projects
        {
            get => _projects; set { _projects = value; RaisePropertyChanged(); }
        }

        private List<Models.Project> _projects;

        public List<Version> Versions
        {
            get => _versions; set { _versions = value; RaisePropertyChanged(); }
        }

        private List<Version> _versions;

        private User _user;

        public RelayCommand<ProjectView> GetData { get; set; }

        public RelayCommand<ProjectView> ModelSelection { get; set; }

        public RelayCommand<ProjectView> ProjectSelection { get; set; }

        public ProjectViewModel(UIApplication uiapp)
        {
            _uiapp = uiapp;
            Model = new ProjectModel();
            WindowLoaded = new RelayCommand<Window>(OnWindowLoaded);
            ModelSelection = new RelayCommand<ProjectView>(OnModelSelection);
            ProjectSelection = new RelayCommand<ProjectView>(OnProjectSelection);
            GetData = new RelayCommand<ProjectView>(OnGetData);
            SendData = new RelayCommand<Window>(OnSendData);
        }

        /// <summary>
        /// The OnWindowLoaded
        /// </summary>
        /// <param name="win">The win<see cref="Window"/></param>
        private async void OnWindowLoaded(Window win)
        {
            Task<User> userTask = Model.GetUser();
            Task<List<ProjectManagement.Models.Project>> projects = Model.GetUserProjectsAsync();
            Documents = _uiapp.Application.Documents;

            User = await userTask;
            Projects = await projects;
        }

        /// <summary>
        /// Get data from database and model
        /// </summary>
        /// <param name="win">The win<see cref="ProjectView"/></param>
        private void OnGetData(ProjectView win)
        {
            RevitElementList.InCloud = new Dictionary<string, RevitElement>();
            foreach (RevitElement e in Model.GetRevitElementInCloud(VersionCommun.CurrentVersion))
            {
                RevitElementList.InCloud.Add(e.guid, e);
            }
            
            Document doc = win.Models.SelectedItem as Document;

            App.ModelHandler._doc = doc;

            Model.GetParamterElement();

             
            win.Close();
           
        }
        private void OnSendData(Window win)
        {
            Model.SendRevitElementToCloud();
        }
        /// <summary>
        /// The OnModelSelection
        /// </summary>
        /// <param name="win">The win<see cref="ProjectView"/></param>
        private void OnModelSelection(ProjectView win)
        {
        }

        /// <summary>
        /// The OnProjectSelection
        /// </summary>
        /// <param name="win">The win<see cref="ProjectView"/></param>
        private async void OnProjectSelection(ProjectView win)
        {
            ProjectManagement.Models.Project selectedProject = win.Projects.SelectedItem as ProjectManagement.Models.Project;
            ProjectProvider.Ins.CurrentProject = selectedProject;
 
            Task<List<ProjectManagement.Models.Version>> versionTask = Model.GetVersionAsync(selectedProject._id);

            List<ProjectManagement.Models.Version> versions = await versionTask;

            Versions = versions;
           
            VersionCommun.CurrentVersion = versions[versions.Count - 1];
        }
    }
}
