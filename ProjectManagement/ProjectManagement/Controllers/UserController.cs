using ProjectManagement.Commun;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Models;
using ProjectManagement.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace ProjectManagement.Controllers
{
   public class UserController
    {
        public static User GetUser()
        {

            string json;
            string jsonPost = "{\"id\":\"" + UserData.authentication.userId + "\"}";

            json = UtilHttp.PostMethodToken(UserRouter.GetUserData, jsonPost, UserData.authentication.token);

            try
            {
                JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                User data = xx.Deserialize<User> (json);

                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }


        }
    }
}
