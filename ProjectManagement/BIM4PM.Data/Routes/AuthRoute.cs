using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public class AuthRoute
    {
        public static string Login = $"{RouteBase.BaseUrl}/auth/login";
        public static string RefreshToken = $"{RouteBase.BaseUrl}/auth/token";
        
    }
}
