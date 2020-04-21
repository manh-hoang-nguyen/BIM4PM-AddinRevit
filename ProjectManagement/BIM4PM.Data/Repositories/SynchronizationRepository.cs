namespace BIM4PM.DataAccess
{
    using BIM4PM.DataAccess.Routes;
    using BIM4PM.Model;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SynchronizationRepository : ISynchronizationRepository
    {
        RestClient _client;

        public SynchronizationRepository()
        {
            _client = RestSharpBase.Client;
        }

        public async Task<SynchroResponse> CreateSynchronization(string modelId, SynchroBody body)
        {
            var route = new SynchronizationRoute(modelId);
            RestSharpBase restSharpBase = new RestSharpBase(route.PostGetUrl, Method.POST);
            RestRequest req = restSharpBase.Request;
            req.AddJsonBody(body);
            var response = await _client.ExecuteAsync<SynchroResponse>(req);
            switch ((int)response.StatusCode)
            {
                case 200:
                    return response.Data;
                default:
                    throw new System.Exception(response.StatusDescription);
            }
        }

        public async Task<Synchronization> GetLastSynchronization(string modelId)
        {
            var url = new SynchronizationRoute(modelId);
            RestSharpBase requestBase = new RestSharpBase(url.GetLastUrl, RestSharp.Method.GET);

            var response = await _client.ExecuteAsync<Synchronization>(requestBase.Request);
            switch ((int)response.StatusCode)
            {
                case 200:
                    return response.Data;
                default:
                    throw new System.Exception(response.StatusDescription);
            }
        }

        public async Task<IEnumerable<Synchronization>> GetSynchronizations(string modelId)
        {
            var url = new SynchronizationRoute(modelId);
            RestSharpBase requestBase = new RestSharpBase(url.PostGetUrl, Method.GET);
            var response = await _client.ExecuteAsync<IEnumerable<Synchronization>>(requestBase.Request);
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
