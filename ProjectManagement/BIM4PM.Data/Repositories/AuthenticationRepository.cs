using BIM4PM.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        
        public Tuple<bool,Token> Login(string email, string password)
        {
            bool isAuthenticated = true;
            RestRequest req = new RestRequest(Route.Login, Method.POST);
            
            string body = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";
            req.RequestFormat = RestSharp.DataFormat.Json; 
            req.AddJsonBody(body);
            IRestResponse<Token> res = Route.Client.Execute<Token>(req); 
            if ((int)res.StatusCode == 401) isAuthenticated = false; 
            return  new Tuple<bool, Token>(isAuthenticated, res.Data);
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
