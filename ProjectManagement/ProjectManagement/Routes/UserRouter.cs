using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Routes
{
   public class UserRouter
    {
        /// <summary>
        /// json: id (of user)
        /// </summary>
        public static string GetUserData = "https://manh-hoang.herokuapp.com/api/user";
        /// <summary>
        /// json: id (of user)
        /// </summary>
        public static string GetUserProject = "https://manh-hoang.herokuapp.com/api/user/project";
    }
}
