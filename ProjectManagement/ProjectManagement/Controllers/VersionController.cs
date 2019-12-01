using ProjectManagement.Commun;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Models;
using ProjectManagement.Routes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using Version = ProjectManagement.Models.Version;

namespace ProjectManagement.Controllers
{
   public class VersionController
    {
        public static List<Version> GetVersion()
        {
            string json;
            string jsonPost = "{\"projectId\":\"" + UserData.idProjectActive + "\"}";

            json = UtilHttp.PostMethodToken(VersionRouter.GetVersion, jsonPost, UserData.authentication.token);

            try
            {
                JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                List<Version> data = xx.Deserialize<List<Version>>(json);

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
