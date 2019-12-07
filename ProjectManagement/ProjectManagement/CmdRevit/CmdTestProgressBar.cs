namespace ProjectManagement.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Newtonsoft.Json;
    using ProjectManagement.Commun;
    using ProjectManagement.Tools.Synchronize;
    using RestSharp;
    using System;

    [Transaction(TransactionMode.ReadOnly)]
    public class CmdTestProgressBar : IExternalCommand
    {
        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="commandData">The commandData<see cref="ExternalCommandData"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <param name="elements">The elements<see cref="ElementSet"/></param>
        /// <returns>The <see cref="Result"/></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
             
            Selection selection = uidoc.Selection;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            string xx = "";
            ICollection<Element> col = new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToElements();
            foreach (Element e in col)
            {
              
                foreach (Parameter para in e.GetOrderedParameters())
                {

                    if (para.IsShared == true)
                    {
                        var xxx = doc.GetElement(para.Id);
                        var sharedParameterElement = doc.GetElement(para.Id) as SharedParameterElement;
                        xx += sharedParameterElement.Name + " : " + sharedParameterElement.GetDefinition().Visible.ToString() + "\n";

                    }
                }
            }
            TaskDialog.Show("revit", xx);
            
        */



            //* Set time out
            //https://stackoverflow.com/questions/48968193/restsharp-the-operation-has-timed-out/49677943
            //https://stackoverflow.com/questions/46584175/restsharp-timeout-not-working
            /* RevitElementRoute route = new RevitElementRoute(ProjectProvider.Ins.CurrentProject._id);
             RestRequest req = new RestRequest(route.url(), Method.POST);
             req.AddHeader("Content-Type", "application/json");
             req.AddHeader("Authorization", "Bearer " + TokenUser.token.token);

             string body = JsonConvert.SerializeObject(RevitElementList.InModel.Values);
             req.RequestFormat = DataFormat.Json;

             req.AddJsonBody(body);

             Route.Client.Timeout = Int32.MaxValue;
             IRestResponse res = Route.Client.Execute(req);
             if (res.StatusCode.ToString() == "OK")
             {
                  TaskDialog.Show("Success", "Operation is finished");
                 return Result.Succeeded;
             }

             if (res.ErrorException != null)
             {
                 string messagex = "Opps! There has been an error while uploading your model. " + res.ErrorException.Message;
                 throw new Exception(messagex);
             }
           */

            //CompareProvider.Instance.Execute();
            SyncView view = new SyncView
            {
                DataContext = new SyncViewModel()
            };
            view.ShowDialog();
            return Result.Succeeded;
        }

        
    }
}
