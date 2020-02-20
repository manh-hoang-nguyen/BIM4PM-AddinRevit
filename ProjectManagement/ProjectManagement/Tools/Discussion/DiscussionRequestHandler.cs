using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectManagement.Commun;
using ProjectManagement.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.Tools.Discussion
{

    public class DiscussionRequestHandler : IExternalEventHandler
    {
        public DiscussionRequest Request { get; set; } = new DiscussionRequest();

        public void Execute(UIApplication app)
        {
            try
            {
                switch (Request.Take())
                {

                    case DiscussionRequestId.Refresh:
                        GetComment(app);
                        break;
                    case DiscussionRequestId.None:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetName()
        {
            return "Discussion External Event";
        }
        private void GetComment(UIApplication app)
        {
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection selection = uidoc.Selection;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            switch (selectedIds.Count)
            {
                case 0:
                    MessageBox.Show("Please select an element");
                    break;
                case 1:
                   
                    Element e = doc.GetElement(selectedIds.First());
                    if ((null != e.Category
                          && 0 < e.Parameters.Size
                          && (e.Category.HasMaterialQuantities)) == false)
                    {
                        MessageBox.Show("This element is NOT SUPPORTED by us. Sorry!");
                        return;
                    }
                    DiscussionProvider.Instance.RevitElement = new RevitElement();
                    DiscussionProvider.Instance.RevitElement.guid = e.UniqueId;
                    DiscussionProvider.Instance.RevitElement.elementId = e.Id.ToString();
                    DiscussionModel.GetComment();
                    break;
                default:
                    MessageBox.Show("Please select ONLY an element");
                    break;
            }
        }
      
    }
    public class DiscussionRequest
    {
        private int _request = (int)DiscussionRequestId.None;

        public DiscussionRequestId Take()
        {
            return (DiscussionRequestId)Interlocked.Exchange(ref _request, (int)DiscussionRequestId.None);
        }

        public void Make(DiscussionRequestId request)
        {
            Interlocked.Exchange(ref _request, (int)request);
        }
    }

    public enum DiscussionRequestId
    {
        None,
        Refresh
    }
}
