namespace BIM4PM.UI.Tools.Auth
{
    using BIM4PM.UI.Commun;
    using BIM4PM.UI.Models;
    using RestSharp;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows;

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
            try
            {
              if (!CheckForInternetConnection())
                {
                    MessageBox.Show("Make sure you have internet");
                    return isAuthenticated;
                }
                HttpStatusCode statusCode;
                RestRequest req = new RestRequest(Route.Login, Method.POST);

                string body = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";
                req.RequestFormat = RestSharp.DataFormat.Json;

                req.AddJsonBody(body);

                IRestResponse<Token> res = Route.Client.Execute<Token>(req);

               
                AuthProvider.Instance.token = new Token
                {
                    token = res.Data.token
                };
                statusCode = res.StatusCode;

                if (statusCode.ToString() == "OK") return isAuthenticated = true;
            }
            catch (System.Exception)
            {
                MessageBox.Show("Sorry! Server error, return later...");
                return isAuthenticated;
            }
           

            return isAuthenticated;
        }
        private bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
