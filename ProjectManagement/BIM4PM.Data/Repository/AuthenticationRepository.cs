using BIM4PM.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public class AuthenticationRepository:IAuthenticationRepository
    {
        public AuthenticationRepository()
        {

        }
        public static string Token {get;set;}
        public bool IsAuthenticated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public Task<IRestResponse<Token>> LoginAsync(string email, string password)
        {
            RestRequest req = new RestRequest(Route.Login, Method.POST);

            string body = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";
            req.RequestFormat = RestSharp.DataFormat.Json;

            req.AddJsonBody(body); 

            return Route.Client.ExecuteAsync<Token>(req);
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
