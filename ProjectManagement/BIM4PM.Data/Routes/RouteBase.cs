using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{

  public class RouteBase
    {
        //public static RestClient Client = new RestClient("https://bim-team.herokuapp.com/");
        public static RestClient Client = new RestClient("http://localhost:5000"); 
        public static string BaseUrl = "/api/v1"; 
    }
  
}
