using Autodesk.Revit.DB;
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

namespace ProjectManagement.Controllers
{
   public class ComparisonController
    {
        public static JsonToPostComparison CreateJsonPost(Element e , string comment, string author)
        {
            
 

            JsonToPostComparison comparison = new JsonToPostComparison()
            {
                
                guid = e.UniqueId,
                comment=comment,
                author=author,
                typeId = e.GetTypeId().ToString(),

                solidVolume = MethodeRevitApi.GetAllSolidVolume(e),
                location = MethodeRevitApi.GetLocationElement(e),

                boundingBox = MethodeRevitApi.GetBoundingBox(e),
                centroidElement = MethodeRevitApi.GetCentroid(e)

            };




            return comparison;
        }
        public static JsonToPostComparison CreateJsonPostFromDocumentChanged(Element e ,string comment, string author)
        {
          

            JsonToPostComparison comparison = new JsonToPostComparison()
            {
                guid = e.UniqueId,
               
                typeId = e.GetTypeId().ToString(),

                solidVolume = MethodeRevitApi.GetAllSolidVolume(e),
                location = MethodeRevitApi.GetLocationElement(e),

                boundingBox = MethodeRevitApi.GetBoundingBox(e),
                centroidElement = MethodeRevitApi.GetCentroid(e)

            };




            return comparison;
        }
        public static Comparison CreateComparison(Element e)
        {
            Comparison comparison = new Comparison()
            { 
                guid = e.UniqueId,
                data = new Data[]
                { new Data {
                                typeId = e.GetTypeId().ToString(),

                                solidVolume = MethodeRevitApi.GetAllSolidVolume(e),
                                location = MethodeRevitApi.GetLocationElement(e),

                                boundingBox = MethodeRevitApi.GetBoundingBox(e),
                                centroidElement = MethodeRevitApi.GetCentroid(e)
                            }
                    
                }
              
            };




            return comparison;
        }
        
        public static void PostComparison_DelElement(string jsonPost)
        { 
            UtilHttp.PostMethodToken(ComparisonRouter.PostDeletedElement, jsonPost, UserData.authentication.token);
            GuidList.guid_deletedElement = new List<string>();
        }
        public static void PostComparison_NewElement(string jsonPost)
        {
            UtilHttp.PostMethodToken(ComparisonRouter.PostNewElement, jsonPost, UserData.authentication.token);
            //JsonPost.PostComparison_NewElement.Clear();
        }
        public static void PostComparison_ModifElement(string jsonPost)
        {
            UtilHttp.PostMethodToken(ComparisonRouter.PostModifiedElement, jsonPost, UserData.authentication.token);
            //JsonPost.PostComparison_ModifiedElement.Clear();
        }

        public static List<Comparison> GetComparison()
        {
            // var json = new WebClient().DownloadString(DataRouter.GetData); //https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
            // Data data= new JavaScriptSerializer().Deserialize<Data>(json);
            string json;
            string jsonPost = "{\"projectId\":\"" + UserData.idProjectActive + "\"}";

            json = UtilHttp.PostMethodToken(ComparisonRouter.GetComparison, jsonPost, UserData.authentication.token); 

            try
            {
                JavaScriptSerializer xx = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                List<Comparison> data = xx.Deserialize<List<Comparison>>(json);

                return data;
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            
        }


    }
}
