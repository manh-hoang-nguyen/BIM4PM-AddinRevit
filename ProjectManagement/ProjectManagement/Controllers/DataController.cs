using Autodesk.Revit.DB;
using ProjectManagement.Routes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows;

namespace ProjectManagement.Controllers
{
    public class DataController
    {/*
        public static Data CreateData(Element e, IList<Level> listLevel)
        { 

            string _volume;
            string _surface;
            string _level = MethodeRevitApi.GetLevelElement(listLevel, e);
            try
            {
                _volume = UtilParameter.GetParameterValue(e.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED));
                _surface = UtilParameter.GetParameterValue(e.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED));
            }
            catch
            {
                _volume = null;
                _surface = null;
            }
            Data data = new Data
            {
                guid = e.UniqueId, 
                status = "new",
                elementId = e.Id.ToString(),
                version = new Version[]
                {
                    new Version
                    {
                        identifiant    =UtilParameter.GetParameterValue( e.get_Parameter(BuiltInParameter.ALL_MODEL_MARK)),
                        level  =MethodeRevitApi.GetLevelElement(listLevel,e),
                        category  =e.Category.Name ,
                        name  =e.Name,
                        volume =_volume ,

                        surface =_surface,

                        typeId=  e.GetTypeId().ToString(),

                        solidVolume =MethodeRevitApi.GetAllSolidVolume(e),
                        location=  MethodeRevitApi.GetLocationElement(e),

                        boundingBox =MethodeRevitApi.GetBoundingBox(e),
                        centroidElement  =MethodeRevitApi.GetCentroid(e)
                    }
                }
            };

            //string json = new JavaScriptSerializer().Serialize(data); 
             
            return data;
        }
        public static void  PostMultipleData(string json)
        {
            string html;
            //https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ComparisonRouter.PostMultipleData);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                
                streamWriter.Write(json);
                MessageBox.Show("Données envoyées avec succèss. Le traitement peut attendre à quelques minutes");
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show("Données envoyées avec succèss.");
                }
            }
            catch(WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();
                MessageBox.Show(html);
            }
          
        }
        public static void ChangeStatusDeletedElement(string json)
        {
            string html;
            //https://stackoverflow.com/questions/9145667/how-to-post-json-to-a-server-using-c
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ComparisonRouter.StatusChange);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {


                streamWriter.Write(json);
               // MessageBox.Show("Données envoyées avec succèss. Le traitement peut attendre à quelques minutes");
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                  //  MessageBox.Show("Données envoyées avec succèss.");
                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                    html = sr.ReadToEnd();
               // MessageBox.Show(html);
            }

        }
        

        public static List<Data> GetData()
        {
            // var json = new WebClient().DownloadString(DataRouter.GetData); //https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            // Data data= new JavaScriptSerializer().Deserialize<Data>(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ComparisonRouter.GetData);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string json = reader.ReadToEnd();
                    //https://stackoverflow.com/questions/22534077/cant-set-maxjsonlength
                    JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                    List<Data> data =   xx.Deserialize<List<Data>>(json);
                     
                    return data;
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
                throw;
            }
        }

        */
    }
    
}