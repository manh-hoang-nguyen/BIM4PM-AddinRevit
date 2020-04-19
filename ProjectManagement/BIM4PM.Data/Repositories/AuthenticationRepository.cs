namespace BIM4PM.DataAccess
{
    using BIM4PM.Model;
    using RestSharp;
    using System;
    using System.Threading.Tasks;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        RestClient _client;

        public AuthenticationRepository()
        {
            _client = RestSharpBase.Client;
        }

        private static Token _token;

        public static Token Token
        {
            get
            {
                var now = DateTime.Now;
                TimeSpan timeSpan = now.Subtract(LoginTime);
                if (timeSpan.Minutes <= 59)
                {
                    return _token;
                }
                else
                {
                    Token token = RefreshToken();
                    Token = token;
                    return token;
                }
            }
            set
            {
                LoginTime = DateTime.Now;
                _token = value;
            }
        }

        static DateTime LoginTime { get; set; }

      

        public bool Login(string email, string password)
        {
            RestRequest req = new RestRequest(AuthRoute.Login, Method.POST);
            string body = "{\"email\":\"" + email + "\",\"" +
                           "password" + "\":\"" + password + "\"}";
            req.RequestFormat = RestSharp.DataFormat.Json;
            req.AddJsonBody(body);
            IRestResponse<Token> res = _client.Execute<Token>(req);
             
            if ((int)res.StatusCode == 200)
            {
                Token = res.Data;
                return true;
            }
            else return false;
        }

        public void Logout()
        {
            Token = null;
        }

        static Token RefreshToken()
        {
            RestRequest req = new RestRequest(AuthRoute.RefreshToken, Method.POST);
            req.AddHeader("token", _token.RefreshToken);
            IRestResponse<Token> response = RouteBase.Client.Execute<Token>(req);
            return response.Data;
        }
    }
}
