namespace ProjectManagement.Tools.Auth
{
    using ProjectManagement.Commun;
    using ProjectManagement.Models;
    using RestSharp;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="LoginModel" />
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="email">The email<see cref="string"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        /// <returns>The <see cref="Task{Token}"/></returns>
        public bool LoginAsync(string email, string password)
        {
            bool isAuthenticated = false;
            HttpStatusCode statusCode;
            RestRequest req = new RestRequest(Route.Login, Method.POST);

            string body = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";


            req.AddJsonBody(body);

            IRestResponse<Token> res = Route.Client.Execute<Token>(req);

            TokenUser.token = new Token
            {
                token = res.Data.token
            };
            statusCode = res.StatusCode;

            if (statusCode.ToString() == "OK") return isAuthenticated = true;

            return isAuthenticated;
        }
    }
}
