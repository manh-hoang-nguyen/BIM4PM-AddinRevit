using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using ProjectManagement.Commun;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Models;
using ProjectManagement.Routes;

namespace ProjectManagement.Controllers
{
    public class HistoryController
    {
        public static List<History> GetHistory()
        {
            
            string json;
            string jsonPost = "{\"projectId\":\"" + UserData.idProjectActive + "\"}";

            json = UtilHttp.PostMethodToken(HistoryRouter.GetHistory, jsonPost, UserData.authentication.token);

            try
            {
                JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                List<History> data = xx.Deserialize<List<History>>(json);

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
