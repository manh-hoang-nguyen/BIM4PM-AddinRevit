using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ProjectManagement.Controllers.Utils
{
   public  class UtilHttp
    {
        public static string GetMethod(string route)
        {
            // var json = new WebClient().DownloadString(DataRouter.GetData); //https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            // Data data= new JavaScriptSerializer().Deserialize<Data>(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(route);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string json = reader.ReadToEnd();
                    //https://stackoverflow.com/questions/22534077/cant-set-maxjsonlength
                    //JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                    //List<Comparison> data = xx.Deserialize<List<Comparison>>(json);

                    return json;
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();

                }
                return "";
            }
        }
        public static string PostMethod(string route,string jsonPost)
        {
            string html;
            //https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(route);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {


                streamWriter.Write(jsonPost);

            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                     

                    return result;

                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();
                
                 
                return "";
            }
        }
        public static string PostMethodToken(string route,string jsonPost, string token)
        {
            string html;
            //https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(route);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {


                streamWriter.Write(jsonPost);

            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd(); 
                    return result;

                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();
              
                return "";
            }

        }
        public static string GetMethodToken(string route, string jsonPost, string token)
        {
            string html;
            //https://stackoverflow.com/questions/3981564/cannot-send-a-content-body-with-this-verb-type
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(route);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;

                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();

                return "";
            }

        }
    }

}
