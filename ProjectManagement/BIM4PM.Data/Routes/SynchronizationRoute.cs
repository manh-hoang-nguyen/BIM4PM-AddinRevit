using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess.Routes
{
   public class SynchronizationRoute
    {
        public SynchronizationRoute(string modelId)
        {
            PostGetUrl = $"{RouteBase.BaseUrl}/models/{modelId}/synchronizations";
            GetLastUrl = $"{PostGetUrl}/last";
        }
        public string PostGetUrl { get; set; } 
        public string GetLastUrl { get; set; }
    }
}
