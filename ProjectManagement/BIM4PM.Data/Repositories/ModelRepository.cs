namespace BIM4PM.DataAccess.Repositories
{
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.DataAccess.Routes;
    using BIM4PM.Model;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ModelRepository : IModelRepository
    {
        RestClient _client;

        public ModelRepository()
        {
            _client = RestSharpBase.Client;
        }

        public async Task<IEnumerable<Model>> GetModels(string projectId)
        {
            var route = new ModelRoute(projectId);
            RestSharpBase restSharpBase = new RestSharpBase(route.GetProjectsUrl, Method.GET);
            IRestResponse<IEnumerable<Model>> response = await _client.ExecuteAsync<IEnumerable<Model>>(restSharpBase.Request);
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
