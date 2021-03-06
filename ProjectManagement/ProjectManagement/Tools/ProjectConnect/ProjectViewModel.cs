﻿namespace BIM4PM.UI.Tools.Project
{
    using Autodesk.Revit.DB;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using BIM4PM.UI.Commun;
    using BIM4PM.UI.Models;
    using BIM4PM.UI.Tools.Synchronize;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ProjectViewModel : ViewModelBase
    {
        private Document _selectedRevitModel;

        public Document SelectedRevitModel
        {
            get { return _selectedRevitModel; }
            set { _selectedRevitModel = value;  RaisePropertyChanged("SelectedRevitModel"); ConnectCommand.RaiseCanExecuteChanged(); }
        }

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { _selectedProject = value; RaisePropertyChanged("SelectedProject"); ConnectCommand.RaiseCanExecuteChanged(); }
        }

        public RelayCommand<ProjectView> WindowLoaded { get; set; }

        public RelayCommand<UserControl> SendData { get; set; }

        public RelayCommand<ProjectView> ConnectCommand { get; set; }

        public RelayCommand<ProjectView> DisconnectCommand { get; set; } 

        public RelayCommand<ProjectView> CompareCommand { get; set; }

        public RelayCommand<ProjectView> SynchronizeCommand { get; set; }

        public ProjectModel Model { get; set; }

        public object User
        {
            get => _user; set { _user = value; RaisePropertyChanged("User"); }
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

        private object _user;

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
            //Model = new ProjectModel();
            WindowLoaded = new RelayCommand<ProjectView>(OnWindowLoaded);

            
            ConnectCommand = new RelayCommand<ProjectView>(OnConnect, CanConnect);
            DisconnectCommand = new RelayCommand<ProjectView>(OnDisconnect, CanDisconnect);
            SendData = new RelayCommand<UserControl>(OnSendData);
            CompareCommand = new RelayCommand<ProjectView>(OnCompare, CanDisconnect);
            SynchronizeCommand = new RelayCommand<ProjectView>(OnSynchronize, CanDisconnect);
        }

        private bool CanDisconnect(ProjectView arg)
        {
             return true;
           
        }

        private bool CanConnect(ProjectView arg)
        {

            //if (ModelProvider.Instance.CurrentModel != null
            //    && ProjectProvider.Instance.CurrentProject != null
            //    && AuthProvider.Instance.IsConnected == false) return true;
            if (SelectedRevitModel != null && SelectedProject!= null) return true;
            return false;
        }

        private void OnSynchronize(ProjectView obj)
        {
            SyncView syncView = new SyncView() { DataContext = new SyncViewModel() };
            syncView.ShowDialog();
        }

        private void OnCompare(ProjectView view)
        {
            if (ModelProvider.DicRevitElements == null || ProjectProvider.Instance.DicRevitElements == null)
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

            //Thread thread = new Thread(() =>
            //{
            ConcurrentDictionary<string, RevitElement> concurDic = new ConcurrentDictionary<string, RevitElement>();
           // Parallel.ForEach(Model.GetRevitElementInCloud(ProjectProvider.Instance.CurrentVersion), e =>
           // {
           //   concurDic.TryAdd(e.guid, e);
           // });
           // if (ProjectProvider.Instance.DicRevitElements != null) ProjectProvider.Instance.DicRevitElements = concurDic;
           //CbModelIsEnable = false;
           // CbProjectIsEnable = false;
            //});
            //thread.Start();

            Model.GetParamterElement();

           
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
        private  void OnWindowLoaded(ProjectView view)
        {
            
                //Task<User> userTask = Model.GetUser();
                //Task<List<Project>> projects = Model.GetProjectsOfUserAsync();
                view.Models.ItemsSource = ModelProvider.Models;
               // User = await userTask;
               // Projects = await projects;

                CbProjectIsEnable = true;
                CbModelIsEnable = true;
                view.EllipseUpToDate.Fill = new SolidColorBrush(Colors.White);
            
        }

        /// <summary>
        /// The OnSendData
        /// </summary>
        /// <param name="win">The win<see cref="UserControl"/></param>
        private void OnSendData(UserControl win)
        {
            //Model.SendRevitElementToCloud();
        }

        
    }
}
