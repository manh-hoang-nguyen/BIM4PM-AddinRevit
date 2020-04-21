namespace BIM4PM.DataAccess
{
    using BIM4PM.Model;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
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
            switch ((int)response.StatusCode)
            {
                case 200:
                    return response.Data; 
                default:
                    throw new Exception(response.ErrorMessage);
            } 
        }

        public async Task<IEnumerable<RevitElement>> GetRevitElementsInPeriod(string versionId,string startDate, string endDate)
        {
            Regex regex = new Regex(@"^\d{4}-((0\d)|(1[012]))-(([012]\d)|3[01])$");
            if(regex.Match(startDate) == Match.Empty || regex.Match(endDate) == Match.Empty)
            {
                throw new FormatException("Bad format of startDate or endDate.");
            }
            var route = new RevitElementRoute(versionId);
            RestSharpBase reqBase = new RestSharpBase(route.GetEleInPeriodUrl, Method.GET);
            RestRequest req = reqBase.Request;
            req.AddQueryParameter("startDate", startDate);
            req.AddQueryParameter("endDate", endDate);

            IRestResponse<IEnumerable<RevitElement>> response = await _client.ExecuteAsync<IEnumerable<RevitElement>>(req);

            switch ((int)response.StatusCode)
            {
                case 200: 
                    return response.Data; 
                default:
                    throw new Exception(response.StatusDescription);
            }
            
        }
    }
}
