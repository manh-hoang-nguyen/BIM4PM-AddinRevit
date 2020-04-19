namespace BIM4PM.DataAccess.Repositories
{
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.DataAccess.Routes;
    using BIM4PM.Model;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GroupProjectRepository : IGroupProjectRepository
    {
        RestClient _client;

        public GroupProjectRepository()
        {
            _client = RestSharpBase.Client;
        }

        public async Task<IEnumerable<GroupProject>> GetGroupProjects()
        {
            var route = new GroupProjectRoute(); 
            RestSharpBase restSharpBase = new RestSharpBase(route.GetUrl, Method.GET); 
            var response = await _client.ExecuteAsync<IEnumerable<GroupProject>>(restSharpBase.Request);
            return response.Data;
        }
    }
}
