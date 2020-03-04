
using Autodesk.Revit.DB;
using BIM4PM.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.DataAccess
{
   public class RevitElementRepository:IRevitElementRepository
    {
        
        public RevitElementRepository()
        {

        }
        public void Delete(IEnumerable<RevitElement> revitElements)
        {
            throw new NotImplementedException();
        }

        public RevitElement GetRevitElement(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RevitElement> GetRevitElements(Project project, ProjectVersion version)
        {
           
            RevitElementRoute route = new RevitElementRoute(project.Id);
            RestRequestBase reqBase = new RestRequestBase(route.url(), Method.GET);
            RestRequest req = reqBase.Request;
            req.AddParameter("version", version.version);
            IRestResponse<RevitElementRes> res = Route.Client.Execute<RevitElementRes>(req);
            string format = "0000-12-31T23:50:39.000Z"; // datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format, Culture = CultureInfo.InvariantCulture };
            
            //RevitElement revitElement = new RevitElement(Element element);
            RevitElementRes revitElements = JsonConvert.DeserializeObject<RevitElementRes>(res.Content, dateTimeConverter);

            return revitElements.data;
        }

        public IEnumerable<RevitElement> GetRevitElements()
        {
            throw new NotImplementedException();
        }

        public void Post(IEnumerable<RevitElement> revitElements)
        {
            throw new NotImplementedException();
        }

        public RevitElement Retrieve(string guid)
        {
            return new RevitElement();
        }

        public List<RevitElement> Retrieve()
        {
            return new List<RevitElement>();
        }

        public bool Save(RevitElement revitElement)
        {
            var success = true;
            if (revitElement.IsValid)
            {
                //Code to save revit element in db
            }
            else
            {
                success = false;
            }
            return success;
        }
      
        

    }
}
