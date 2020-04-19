using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public class RestSharpBase
    { 
        //public static RestClient Client = new RestClient("https://bim-team.herokuapp.com/");
        public static RestClient Client = new RestClient("http://localhost:5000");

        public RestSharpBase(string Url, Method method)
        {
            Request = new RestRequest(Url, method);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddHeader("Authorization", "Bearer " + AuthenticationRepository.Token.AccessToken);
        }

        public RestRequest Request { get; set; }

    }
}
