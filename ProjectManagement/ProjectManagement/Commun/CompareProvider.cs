namespace ProjectManagement.Commun
{
    using Autodesk.Revit.DB;
    using Newtonsoft.Json;
    using ProjectManagement.Models;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    public class CompareProvider
    {
        private readonly static CompareProvider _instance = new CompareProvider();
        private CompareProvider()
        {
            ElementToExamine = new List<ElementId>();
            Deleted = new List<string>();
            New = new List<string>();

            Modified = new List<string>();

            Same = new List<string>();
        }

        public static CompareProvider Instance => _instance;
       

        public IList<ElementId> ElementToExamine { get; set; } = new List<ElementId>();

        public Dictionary<string, RevitElement> ModifiedElementToSynchonize { get; set; }

        public IList<string> Deleted { get; set; }

        public IList<string> New { get; set; }

        public ICollection<string> Modified { get; set; }

        public ICollection<string> Same { get; set; }

        /// <summary>
        /// Verify if model is up to date on cloud (after compare)
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsUpToDate()
        {
            if (Modified.Count == 0 && Deleted.Count == 0 && New.Count == 0 && Same.Count != 0) return true;
            else return false;
        }

        /// <summary>
        /// Reset compare when disconnect or logout
        /// </summary>
        public void Reset()
        {
            ElementToExamine = new List<ElementId>();
            Deleted = new List<string>();
            Modified = new List<string>();
            New = new List<string>();
            Same = new List<string>();
            ModifiedElementToSynchonize = null;
        }

        /// <summary>
        /// Execute compare, find same, modified, delete, new element and create a list of element to synchronize on cloud
        /// </summary>
        public void Execute()
        {
            ModifiedElementToSynchonize = new Dictionary<string, RevitElement>();
            Modified = new List<string>();
            Same = new List<string>();
            Deleted = new List<string>();
            New = new List<string>();

            Deleted = ProjectProvider.Instance.DicRevitElements.Keys.Except(ModelProvider.Instance.DicRevitElements.Keys).ToList();
            New = ModelProvider.Instance.DicRevitElements.Keys.Except(ProjectProvider.Instance.DicRevitElements.Keys).ToList();

            var EleToCompareGuid = ModelProvider.Instance.DicRevitElements.Keys.Except(New);
            //Parallel.ForEach(EleToCompareGuid, guid =>
            foreach (string guid in EleToCompareGuid)

            {
                History history = new History();
                RevitElement current = ModelProvider.Instance.DicRevitElements[guid];
                RevitElement previous = ProjectProvider.Instance.DicRevitElements[guid];
                bool geometryIsSame = GeometryCompare(current, previous);
                bool revitParameterIsSame = ParameterCompare(current, previous);
                bool sharedParameterIsSame = SharedParameterCompare(current, previous);

                if (geometryIsSame && revitParameterIsSame && sharedParameterIsSame)
                {
                    Same.Add(guid);
                }
                else
                {
                    Modified.Add(guid);
                    if (!geometryIsSame) history.geometryChange = true;
                    if (!revitParameterIsSame) history.parameterChange = true;
                    if (!sharedParameterIsSame) history.sharedParameterChange = true;
                    RevitElement revitElement = new RevitElement(previous, current, new List<History> { history });

                    ModifiedElementToSynchonize.Add(guid, revitElement);
                }
            };
        }

        /// <summary>
        /// The Synchronize
        /// </summary>
        public async void Synchronize()
        {
            Task task1 = ModifiedElementAsync();
            Task task2 = DeletedElementAsync();
            Task task3 = NewElementAsync();

            await Task.WhenAll(task1, task2, task3);
            ProjectProvider.Instance.Update();
            MessageBox.Show("Sucess! Your model is updated on cloud.If your data is too big, our server can take a while to process");
        }

        /// <summary>
        /// The FirstCommit
        /// </summary>
        public async void FirstCommit()
        {

            Task task = NewElementAsync();
            await Task.WhenAll(task);
            MessageBox.Show("Sucess! Your model is sent to cloud.If your data is too big, our server can take a while to process");
        }

        /// <summary>
        /// The ModifiedElementAsync
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        private async Task ModifiedElementAsync()
        {
            if (Modified.Count == 0) return;

            RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(), Method.PUT);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            string body = JsonConvert.SerializeObject(ModifiedElementToSynchonize.Values);
            req.RequestFormat = RestSharp.DataFormat.Json;
            req.AddJsonBody(body);
            Route.Client.Timeout = Int32.MaxValue;
            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);
            var res = await resTask;
        }

        /// <summary>
        /// The DeletedElementAsync
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        private async Task DeletedElementAsync()
        {
            if (ProjectProvider.Instance.CurrentProject == null
                || Deleted.Count == 0
                || AuthProvider.Instance.token == null) return;
            List<RevitElement> delElements = new List<RevitElement>();
            foreach (string guid in Deleted)
            {
                delElements.Add(ProjectProvider.Instance.DicRevitElements[guid]);
            }
            RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(), Method.DELETE);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            string body = JsonConvert.SerializeObject(delElements);
            req.RequestFormat = RestSharp.DataFormat.Json;
            req.AddJsonBody(body);
            Route.Client.Timeout = Int32.MaxValue;
            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);
            var res = await resTask;
        }

        /// <summary>
        /// The NewElementAsync
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        private async Task NewElementAsync()
        {
            if (ProjectProvider.Instance.CurrentProject == null
               || New.Count == 0
               || AuthProvider.Instance.token == null) return;
            List<RevitElement> newElements = new List<RevitElement>();
            foreach (string guid in New)
            {
                newElements.Add(ModelProvider.Instance.DicRevitElements[guid]);
            }

            RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(), Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            string body = JsonConvert.SerializeObject(newElements);
            req.RequestFormat = RestSharp.DataFormat.Json;
            req.AddJsonBody(body);
            Route.Client.Timeout = Int32.MaxValue;
            Task<IRestResponse> resTask = Route.Client.ExecuteTaskAsync(req);

            var res = await resTask;
        }

        /// <summary>
        /// The GeometryCompare
        /// </summary>
        /// <param name="current">The current<see cref="RevitElement"/></param>
        /// <param name="previous">The previous<see cref="RevitElement"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool GeometryCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.boundingBox == previous.boundingBox
                && current.centroid == previous.centroid
                && current.location == previous.location
                && current.geometryParameters == previous.geometryParameters
                && current.volume == previous.volume) result = true;

            return result;
        }

        /// <summary>
        /// The ParameterCompare
        /// </summary>
        /// <param name="current">The current<see cref="RevitElement"/></param>
        /// <param name="previous">The previous<see cref="RevitElement"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ParameterCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.parameters == previous.parameters) result = true;

            return result;
        }

        /// <summary>
        /// The SharedParameterCompare
        /// </summary>
        /// <param name="current">The current<see cref="RevitElement"/></param>
        /// <param name="previous">The previous<see cref="RevitElement"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool SharedParameterCompare(RevitElement current, RevitElement previous)
        {
            bool result = false;

            if (current.sharedParameters == previous.sharedParameters) result = true;

            return result;
        }
    }
}
