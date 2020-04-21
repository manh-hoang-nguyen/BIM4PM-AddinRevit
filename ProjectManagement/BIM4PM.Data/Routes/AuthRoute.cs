using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public class AuthRoute
    {
        public static string Login = $"{RouteBase.ApiPrefix}/auth/login";
        public static string RefreshToken = $"{RouteBase.ApiPrefix}/auth/token";
        
    }
}
