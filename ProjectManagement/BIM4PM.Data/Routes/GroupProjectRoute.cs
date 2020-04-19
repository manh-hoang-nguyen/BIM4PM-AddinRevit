using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess.Routes
{
   public class GroupProjectRoute
    {
       
        public string GetUrl { get; set; } = $"{RouteBase.BaseUrl}/groupprojects";
    }
}
