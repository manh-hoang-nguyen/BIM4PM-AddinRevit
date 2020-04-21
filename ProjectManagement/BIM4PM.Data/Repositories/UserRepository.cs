namespace BIM4PM.DataAccess
{
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.DataAccess.Routes;
    using BIM4PM.Model;
    using RestSharp;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        RestClient _client;

        public UserRepository()
        {
            _client = RestSharpBase.Client;
        }

        public async Task<User> GetMeAsync()
        {
            RestSharpBase reqBase = new RestSharpBase(UserRoute.GetMe, Method.GET);
            RestRequest req = reqBase.Request;
            IRestResponse<User> response = await _client.ExecuteAsync<User>(req);
            switch ((int)response.StatusCode)
            {
                case 200:
                    return response.Data;
                default:
                    throw new System.Exception(response.StatusDescription);
            }
            
        }
    }
}
