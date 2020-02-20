namespace ProjectManagement.Controllers
{
    using ProjectManagement.Commun;
    using ProjectManagement.Controllers.Utils;
    using ProjectManagement.Routes;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Script.Serialization;
    using System.Windows;
    using Version = ProjectManagement.Models.Version;

    public class VersionController
    {
        /// <summary>
        /// The GetVersion
        /// </summary>
        /// <returns>The <see cref="ObservableCollection{Version}"/></returns>
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
