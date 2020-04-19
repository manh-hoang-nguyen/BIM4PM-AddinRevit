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
        public async Task<ProjectVersion> GetCurrentVersion(string modelId)
        {
            var route = new VersionRoute();
            RestSharpBase requestBase = new RestSharpBase(route.GetCurrentVersion, Method.GET);
            RestRequest req = requestBase.Request;
            req.AddHeader("modelid", modelId);
            IRestResponse<ProjectVersion> response = await _client.ExecuteAsync<ProjectVersion>(req);
            return response.Data;
        }

        public async Task<IEnumerable<ProjectVersion>> GetVersions(string modelId)
        {
            var route = new VersionRoute();
            RestSharpBase requestBase = new RestSharpBase(route.GetVersions, Method.GET);
            RestRequest req = requestBase.Request;
            req.AddHeader("modelid", modelId);
            IRestResponse<IEnumerable<ProjectVersion>> response = await _client.ExecuteAsync<IEnumerable<ProjectVersion>>(req);
            return response.Data;
        }
    }
}
