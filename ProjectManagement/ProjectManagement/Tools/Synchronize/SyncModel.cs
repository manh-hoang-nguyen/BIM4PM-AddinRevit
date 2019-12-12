namespace ProjectManagement.Tools.Synchronize
{
    using Newtonsoft.Json;
    using ProjectManagement.Commun;
    using RestSharp;
    using System;
    using System.Windows.Forms;

    public class SyncModel
    {
        public SyncModel()
        {
        }

        /// <summary>
        /// The FirstCommit
        /// </summary>
        public void FirstCommit()
        {
            //https://stackoverflow.com/questions/48968193/restsharp-the-operation-has-timed-out/49677943
            //https://stackoverflow.com/questions/46584175/restsharp-timeout-not-working
            RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(), Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

            string body = JsonConvert.SerializeObject(ModelProvider.Instance.DicRevitElements.Values);
            req.RequestFormat = DataFormat.Json;

            req.AddJsonBody(body);

            Route.Client.Timeout = Int32.MaxValue;
            IRestResponse res = Route.Client.Execute(req);
            if (res.StatusCode.ToString() == "OK")
            {
                MessageBox.Show("Success", "Operation is finished. Please reconnect to project!");
                AuthProvider.Instance.Disconnect();
                return;
            }

            if (res.ErrorException != null)
            {
                string message = "Opps! There has been an error while uploading your model. " + res.ErrorException.Message;
                throw new Exception(message);
            }
            AuthProvider.Instance.Disconnect();
        }

        /// <summary>
        /// The Synchronize
        /// </summary>
        public void Synchronize()
        {
            CompareProvider.Instance.Synchronize();
        }
    }
}
