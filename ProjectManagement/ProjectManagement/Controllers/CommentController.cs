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
   public class CommentController
    {
        public static ChildComment PostComment(string json, string token)
        {
            string html;
            //https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(CommentRouter.PostComment);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {


                streamWriter.Write(json);
                
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                    ChildComment data = xx.Deserialize<ChildComment>(result);

                    return data;

                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();
                MessageBox.Show(html);
                ChildComment data = null;
                return data;
            }

        }

        public static List<Comment> GetAllCommentInProject()
        {

            string json;
            string jsonPost = "{\"projectId\":\"" + UserData.idProjectActive + "\"}";

            json = UtilHttp.PostMethodToken(CommentRouter.GetAllComments, jsonPost, UserData.authentication.token);

            try
            {
                JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                List<Comment> data = xx.Deserialize<List<Comment>>(json);

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
