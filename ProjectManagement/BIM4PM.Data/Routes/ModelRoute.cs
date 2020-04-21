using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess.Routes
{
   public class ModelRoute
    {
        public ModelRoute(string projectId)
        {
            GetProjectsUrl = $"{RouteBase.ApiPrefix}/projects/{projectId}/models";
        }
        public string GetProjectsUrl { get; set; }
    }
}
