namespace BIM4PM.DataAccess
{
    using BIM4PM.Model;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RevitElementRepository : IRevitElementRepository
    {
        RestClient _client;
        public RevitElementRepository()
        {
            _client = RestSharpBase.Client;
        }
        public async Task<IEnumerable<RevitElement>> GetAllElementsOfVersion(string versionId)
        {
            var route = new RevitElementRoute(versionId);
            RestSharpBase reqBase = new RestSharpBase(route.GetElementsUrl, Method.GET);
            IRestResponse<IEnumerable<RevitElement>> response = await _client.ExecuteAsync<IEnumerable<RevitElement>>(reqBase.Request);
            return response.Data;
        }

        public async Task<IEnumerable<RevitElement>> GetRevitElementsInPeriod(string versionId,string startDate, string endDate)
        {
            var route = new RevitElementRoute(versionId);
            RestSharpBase reqBase = new RestSharpBase(route.GetEleInPeriodUrl, Method.GET);
            RestRequest req = reqBase.Request;
            req.AddQueryParameter("startDate", startDate);
            req.AddQueryParameter("endDate", endDate);

            IRestResponse<IEnumerable<RevitElement>> response = await _client.ExecuteAsync<IEnumerable<RevitElement>>(req);

            if((int)response.StatusCode == 400)
            {
                throw new ArgumentException("StartDate and endDate are similar.");
            }
            else return response.Data;
        }
    }
}
