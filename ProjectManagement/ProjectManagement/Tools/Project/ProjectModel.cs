namespace ProjectManagement.Tools.Project
{
    using Autodesk.Revit.DB;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using RestSharp;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows;
    using DataFormat = RestSharp.DataFormat;

    public class ProjectModel
    {
        

        public ProjectModel()
        {
        }

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <returns>The <see cref="Task{User}"/></returns>
        public async Task<User> GetUser()
        {
            RestRequest req = new RestRequest(Route.GetMe, Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);

            var res = await resTask;
            UserRes User = JsonConvert.DeserializeObject<UserRes>(res.Content);
            return User.data;
        }

        /// <summary>
        /// The GetUserProjects
        /// </summary>
        /// <returns>The <see cref="Task{List{ProjectManagement.Models.Project}}"/></returns>
        public async Task<List<ProjectManagement.Models.Project>> GetUserProjectsAsync()
        {
            RestRequest req = new RestRequest(Route.UserProjects, Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);
            var res = await resTask;
            ProjectRes Project = JsonConvert.DeserializeObject<ProjectRes>(res.Content);
            return Project.data;
        }

        /// <summary>
        /// The GetProjectById
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{ProjectManagement.Models.Project}}"/></returns>
        public async Task<List<ProjectManagement.Models.Version>> GetVersionAsync(string id)
        {
            string url = string.Format("{0}/{1}/versions", Route.UserProjects, id);
            RestRequest req = new RestRequest(url, Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);
            var res = await resTask;
            VersionRes Version = JsonConvert.DeserializeObject<VersionRes>(res.Content);
            return Version.data[0].versions;
        }

        /// <summary>
        /// The getUser
        /// </summary>
        /// <returns>The <see cref="UserRes"/></returns>
        public UserRes getUser()
        {
            try
            {
                RestRequest req = new RestRequest(Route.GetMe, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
                IRestResponse<UserRes> resTask = Route.Client.Execute<UserRes>(req);
                UserRes account = JsonConvert.DeserializeObject<UserRes>(resTask.Content);

                return resTask.Data;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// The DocumentSet
        /// </summary>
        public void DocumentSet()
        {
            App.ModelHandler.Request.Make(RequestId.Model);
            App.ModelEvent.Raise();
        }

        /// <summary>
        /// The GetParamterElement
        /// </summary>
        public void GetParamterElement()
        {
            App.ModelHandler.Request.Make(RequestId.Element);
            App.ModelEvent.Raise();
        }

        /// <summary>
        /// The GetRevitElementInCloud
        /// </summary>
        /// <param name="version">The version<see cref="Version"/></param>
        /// <returns>The <see cref="List{RevitElement}"/></returns>
        public List<RevitElement> GetRevitElementInCloud(Version version)
        {
           return ProjectProvider.Instance.GetRevitElementInCloud(version);
        }

        /// <summary>
        /// The SendRevitElementToCloud
        /// </summary>
        public void SendRevitElementToCloud()
        {
            RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
            if (ProjectProvider.Instance.CurrentProject._id == null)
            {
                MessageBox.Show("Select a project please");
                return;
            }

            RestRequest req = new RestRequest(route.url(), Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

            string body = JsonConvert.SerializeObject(ModelProvider.Instance.DicRevitElements);
            req.RequestFormat = DataFormat.Json;

            req.AddJsonBody(body);

            IRestResponse res = Route.Client.Execute(req);
        }
    }
}
