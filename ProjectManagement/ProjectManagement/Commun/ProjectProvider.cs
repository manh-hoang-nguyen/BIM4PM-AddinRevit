namespace BIM4PM.UI.Commun
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using BIM4PM.UI.Models;
    using RestSharp;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class ProjectProvider : INotifyPropertyChanged
    {
        private ProjectProvider()
        {
            DicRevitElements = new ConcurrentDictionary<string, RevitElement>();
           
        }

        public static ProjectProvider Instance { get; } = new ProjectProvider();

        private Project _currentProject;

        public List<Member> ProjectMembers;

        public IList<Project> UserProjects { get; set; }

        public ConcurrentDictionary<string, RevitElement> DicRevitElements { get; set; }

        public List<Version> Versions { get; set; }

        public Project CurrentProject
        {
            get => _currentProject; set { _currentProject = value; OnPropertyChanged(); }
        }

        public Project SelectedProject { get; set; }

        public Version CurrentVersion;

        public event PropertyChangedEventHandler PropertyChanged;

        
        private void Reset()
        {
             
                ProjectMembers = null;
                SelectedProject = null;
                CurrentProject = null;
                DicRevitElements = null;
                Versions = null;
                UserProjects = null;
                CurrentVersion = null;
            
        }

        /// <summary>
        /// Update DicRevitElements after synchronized
        /// </summary>
        public void Update()
        {
            DicRevitElements = new ConcurrentDictionary<string, RevitElement>();
            if (CurrentVersion == null)
            {
                MessageBox.Show("Current version null, please select a project");
                return;
            }
            foreach (RevitElement e in GetRevitElementInCloud(CurrentVersion))
            {
                DicRevitElements.TryAdd(e.guid, e);
            }
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (CurrentProject != null)
            {
                 

                if (Versions.Count == 0)
                {
                    MessageBox.Show("Please create a version of project.");
                    return;
                }
                CurrentVersion = Versions[Versions.Count - 1];

                Thread thread = new Thread(() =>
                {
                     
                    ProjectMembers = SelectedProject.members;
                });
                thread.Start();
            }
        }
 
        public List<RevitElement> GetRevitElementInCloud(Version version)
        {
            if (CurrentProject == null)
            {
                MessageBox.Show("Select a project please");
                return new List<RevitElement>();
            }
            

            return null;
        }
    }
}
