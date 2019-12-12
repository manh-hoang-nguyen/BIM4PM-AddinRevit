namespace ProjectManagement.Tools.Discussion
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using RestSharp;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;

    public class DiscussionModel
    {
         
       

        public void Refresh()
        {
            App.DiscussionHandler.Request.Make(DiscussionRequestId.Refresh);
            App.DiscussionEvent.Raise();
        }

        public void SendComment(string text)
        {
          
            if (DiscussionProvider.Instance.RevitElement == null)
            {
                MessageBox.Show("You have to press the button <Comments> first.");
                return;
            }
            string guid = DiscussionProvider.Instance.RevitElement.guid;
            CommentRoute route = new CommentRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(guid), Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            string body = "{\"text\":\"" + text + "\"}";
            req.RequestFormat = RestSharp.DataFormat.Json;
            req.AddJsonBody(body);
            IRestResponse res = Route.Client.Execute(req);
            if(res.StatusCode.ToString() == "OK")
            {
                string format = "0000-12-31T23:50:39.000Z";
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format, Culture = CultureInfo.InvariantCulture };
                CommentResPost commentRes = JsonConvert.DeserializeObject<CommentResPost>(res.Content, dateTimeConverter);
                Comment comment = commentRes.data;

                DiscussionProvider.Instance.Comments.Insert(0, comment);
            }
            else
            {
                MessageBox.Show("Error! Can not post comment.");
            }
        }

        public static void GetComment()
        {
            string guid = DiscussionProvider.Instance.RevitElement.guid;
            if (guid == null)
            {
                MessageBox.Show("You have to refresh first");
                return;
            }
            DiscussionProvider.Instance.Comments.Clear();

            CommentRoute route = new CommentRoute(ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(route.url(guid), Method.GET);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            IRestResponse res = Route.Client.Execute(req);
            string format = "0000-12-31T23:50:39.000Z";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format, Culture = CultureInfo.InvariantCulture };
            CommentRes commentRes = JsonConvert.DeserializeObject<CommentRes>(res.Content, dateTimeConverter);

            if (commentRes.data != null)
            {
                ObservableCollection<Comment> tmp = new ObservableCollection<Comment>();
                foreach (Comment comment in commentRes.data.comments)
                {
                    tmp.Add(comment);
                }
                for (int i = 1; i <= tmp.Count(); i++)
                {
                    DiscussionProvider.Instance.Comments.Add(tmp[tmp.Count() - i]);
                }
            }
        }
    }
}
