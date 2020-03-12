namespace ProjectManagement.Commun
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using ProjectManagement.Models;
    using RestSharp;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class ProjectProvider : INotifyPropertyChanged
    {
        private readonly static ProjectProvider _instance = new ProjectProvider();

        private ProjectProvider()
        {
            AuthProvider.Instance.ConnectionChanged += (s, e) => Reset();
        }

        public static ProjectProvider Instance => _instance;

        private Project _currentProject;

        public List<Member> ProjectMembers;

        public IList<Project> UserProjects { get; set; }

        public Dictionary<string, RevitElement> DicRevitElements { get; set; }

        public List<Version> Versions { get; set; }

        public Project CurrentProject
        {
            get => _currentProject; set { _currentProject = value; OnPropertyChanged(); }
        }

        public Project SelectedProject { get; set; }

        public Version CurrentVersion;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The Reset
        /// </summary>
        private void Reset()
        {
            if (AuthProvider.Instance.IsConnected == false)
            {
                ProjectMembers = null;
                SelectedProject = null;
                CurrentProject = null;
                DicRevitElements = null;
                Versions = null;
                UserProjects = null;
                CurrentVersion = null;
            }
        }

        /// <summary>
        /// Update DicRevitElements after synchronized
        /// </summary>
        public void Update()
        {
            DicRevitElements = new Dictionary<string, RevitElement>();
            if (CurrentVersion == null)
            {
                MessageBox.Show("Current version null, please select a project");
                return;
            }
            foreach (RevitElement e in GetRevitElementInCloud(CurrentVersion))
            {
                DicRevitElements.Add(e.guid, e);
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
                string url = string.Format("{0}/{1}/versions", Route.UserProjects, CurrentProject._id);
                RestRequest req = new RestRequest(url, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
                IRestResponse res = Route.Client.Execute(req);

                VersionRes Version = JsonConvert.DeserializeObject<VersionRes>(res.Content);
                Versions = Version.data[0].versions;

                if (Versions.Count == 0)
                {
                    MessageBox.Show("Please create a version of project.");
                    return;
                }
                CurrentVersion = Versions[Versions.Count - 1];

                Thread thread = new Thread(() =>
                {
                    //Get project information
                    string url0 = string.Format("{0}/{1}", Route.UserProjects, CurrentProject._id);
                    RestRequest req0 = new RestRequest(url0, Method.GET);
                    req0.AddHeader("Content-Type", "application/json");
                    req0.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
                    IRestResponse res0 = Route.Client.Execute(req0);
                    SingleProjectRes project = JsonConvert.DeserializeObject<SingleProjectRes>(res0.Content);
                    SelectedProject = project.data;
                    ProjectMembers = SelectedProject.members;
                });
                thread.Start();
            }
        }

        /// <summary>
        /// The GetRevitElementInCloud
        /// </summary>
        /// <param name="version">The version<see cref="Version"/></param>
        /// <returns>The <see cref="List{RevitElement}"/></returns>
        public List<RevitElement> GetRevitElementInCloud(Version version)
        {
            if (CurrentProject == null)
            {
                MessageBox.Show("Select a project please");
                return new List<RevitElement>();
            }
            RevitElementRoute route = new RevitElementRoute(CurrentProject._id);
            RestRequest req = new RestRequest(route.url(), Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            req.AddParameter("version", version.version);
            IRestResponse<RevitElementRes> res = Route.Client.Execute<RevitElementRes>(req);
            string format = "0000-12-31T23:50:39.000Z"; // datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format, Culture = CultureInfo.InvariantCulture };

            RevitElementRes revitElements = JsonConvert.DeserializeObject<RevitElementRes>(res.Content, dateTimeConverter);

            return revitElements.data;
        }
    }
}
