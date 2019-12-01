using ProjectManagement.Controllers.Utils;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ProjectManagement.Controllers
{
   public class UserProjectController
    {
        public static List<UserProject> GetUserProject(string route, string token, string userId)
        {
             
            string json;
            string jsonPost = "{\"id\":\"" + userId + "\"}";

            json = UtilHttp.PostMethodToken(route, jsonPost,token);
            JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

            List<UserProject> userProject = xx.Deserialize<List<UserProject>>(json);


            return userProject;
        }
    }
}
