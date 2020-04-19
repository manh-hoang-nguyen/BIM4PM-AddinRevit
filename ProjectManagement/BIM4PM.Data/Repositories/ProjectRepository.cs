using BIM4PM.DataAccess.Routes;
using BIM4PM.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
    public class ProjectRepository : IProjectRepository
    {
        RestClient _client;
        public ProjectRepository()
        {
            _client = RestSharpBase.Client;
        }
        public async Task<IEnumerable<Project>> GetProjects()
        {
            RestSharpBase requestBase = new RestSharpBase(ProjectRoute.ProjectsOfUserUrl, Method.GET);
            IRestResponse<IEnumerable<Project>> response = await _client.ExecuteAsync<IEnumerable<Project>>(requestBase.Request);
            return response.Data;
        }
    }
}
