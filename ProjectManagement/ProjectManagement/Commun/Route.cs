using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
  public class Route
    {
        public static RestClient Client = new RestClient("https://manhhoang-api.herokuapp.com");
        //public static RestClient Client = new RestClient(" http://localhost:5000");

        public static string Login = "api/v1/auth/login";
        public static string GetMe = "/api/v1/auth/me";
        public static string UserProjects = "/api/v1/projects"; 
    }
    public class RevitElementRoute
    {
        public string projectId;
       
        public RevitElementRoute(string projectId)
        {
            this.projectId = projectId;
        }
        public string url()
        {
           string url = "/api/v1/projects/" + projectId +"/elements";

           return url;
        }
        public string historyUrl(string guid)
        {
            string url = "/api/v1/projects/" + projectId + "/elements/guid/" + guid + "/history";

            return url;
        }
       
    }
    public class CommentRoute
    {
        public string projectId;

        public CommentRoute(string projectId)
        {
            this.projectId = projectId;
        }
        public string url(string guid)
        {

            string url = "/api/v1/projects/" + projectId + "/comments/guid/" + guid;

            return url;
        }
        public string updateUrl(string guid, string id)
        {

            string url = "/api/v1/projects/" + projectId + "/comments/" + id + "/guid/" + guid;

            return url;
        }
    }
   public class TopicRoute
    {
        public string projectId;

        public TopicRoute(string projectId)
        {
            this.projectId = projectId;
        }
        public string url(string guid)
        {

            string url = "/api/v1/projects/" + projectId + "/topics/guid/" + guid;

            return url;
        }
        public string updateUrl(string guid, string id)
        {

            string url = "/api/v1/projects/" + projectId + "/topics/" + id + "/guid/" + guid;

            return url;
        }
    }

}
