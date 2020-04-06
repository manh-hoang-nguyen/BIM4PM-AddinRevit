using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public class RestRequestBase
    { 
        public RestRequestBase(string Url, Method method)
        {
            Request = new RestRequest(Url, method);
            Request.AddHeader("Content-Type", "application/json");
            Request.AddHeader("Authorization", "Bearer " + AuthenticationRepository.Token);
        }

        public RestRequest Request { get; set; }
    }
}
