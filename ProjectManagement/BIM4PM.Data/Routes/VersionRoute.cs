using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess.Routes
{
   public class VersionRoute
    {
        public VersionRoute()
        {
            GetVersions = $"{RouteBase.ApiPrefix}/versions";
            GetCurrentVersion = $"{GetVersions}/current";
        }
        public string GetVersions { get; set; }
        public string GetCurrentVersion { get; set; }
    }
}
