namespace BIM4PM.DataAccess
{
    using BIM4PM.DataAccess.Interfaces;
    using BIM4PM.DataAccess.Routes;
    using BIM4PM.Model;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VersionRepository : IVersionRepository
    {
        RestClient _client;
        public VersionRepository()
        {
            _client = RestSharpBase.Client;
        }
        public async Task<ModelVersion> GetCurrentVersion(string modelId)
        {
            var route = new VersionRoute();
            RestSharpBase requestBase = new RestSharpBase(route.GetCurrentVersion, Method.GET);
            RestRequest req = requestBase.Request;
            req.AddHeader("modelid", modelId);
            IRestResponse<ModelVersion> response = await _client.ExecuteAsync<ModelVersion>(req);
            return response.Data;
        }

        public async Task<IEnumerable<ModelVersion>> GetVersions(string modelId)
        {
            var route = new VersionRoute();
            RestSharpBase requestBase = new RestSharpBase(route.GetVersions, Method.GET);
            RestRequest req = requestBase.Request;
            req.AddHeader("modelid", modelId);
            IRestResponse<IEnumerable<ModelVersion>> response = await _client.ExecuteAsync<IEnumerable<ModelVersion>>(req);
            return response.Data;
        }
    }
}
