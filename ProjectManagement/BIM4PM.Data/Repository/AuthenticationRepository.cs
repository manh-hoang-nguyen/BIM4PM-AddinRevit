using BIM4PM.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public class AuthenticationRepository
    {
        public AuthenticationRepository()
        {

        }
        public static string Token {get;set;}
         
        public bool Login(string email, string password)
        {
            bool isAuthenticated = true;
            RestRequest req = new RestRequest(Route.Login, Method.POST);

            string body = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";
            req.RequestFormat = RestSharp.DataFormat.Json;

            req.AddJsonBody(body);

            IRestResponse<Token> res = Route.Client.Execute<Token>(req);
            if (res.Data.success == false) isAuthenticated = false;
            else Token = res.Data.token;
            return isAuthenticated;
        }
    }
}
