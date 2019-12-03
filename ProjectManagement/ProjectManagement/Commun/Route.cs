using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
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
    }
}
