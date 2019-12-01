using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Models;
using ProjectManagement.Routes;

namespace ProjectManagement.Controllers
{
   public  class LoginController
    {
        public static Authentication GetTokenAndUserId(string email, string password)
        {
            string json;
            string jsonPost = "{\"email\":\"" + email + "\",\"" + "password" + "\":\"" + password + "\"}";
            json=UtilHttp.PostMethod(LoginRouter.Login,jsonPost);
            JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
             
            Authentication authentication = xx.Deserialize<Authentication>(json);
            return authentication;
        }

    }
}
