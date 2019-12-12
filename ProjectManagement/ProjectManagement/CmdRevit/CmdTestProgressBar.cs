namespace ProjectManagement.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Newtonsoft.Json;
    using ProjectManagement.Commun;
    using ProjectManagement.Tools.History;
    using ProjectManagement.Tools.Synchronize;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.IO;

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
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            /*
           
             
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
            //FilteredElementCollector col
            //= new FilteredElementCollector(doc)
            //.OfClass(typeof(ViewSchedule));

            //ViewScheduleExportOptions opt
            //  = new ViewScheduleExportOptions();

            //foreach (ViewSchedule vs in col)
            //{
            // var x=   vs.Parameters;
            //  var y=  vs.Name;
            //}

            getScheduleData(doc);
            return Result.Succeeded;
        }

        public void getScheduleData(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> collection = collector.OfClass(typeof(ViewSchedule)).ToElements();

            String prompt = "ScheduleData :";
            prompt += Environment.NewLine;

            foreach (Element e in collection)
            {
                ViewSchedule viewSchedule = e as ViewSchedule;
                TableData table = viewSchedule.GetTableData();
                TableSectionData section = table.GetSectionData(SectionType.Body);
                int nRows = section.NumberOfRows;
                int nColumns = section.NumberOfColumns;

                if (nRows > 1)
                {
                    //valueData.Add(viewSchedule.Name);

                    List<List<string>> scheduleData = new List<List<string>>();
                    for (int i = 0; i < nRows; i++)
                    {
                        List<string> rowData = new List<string>();

                        for (int j = 0; j < nColumns; j++)
                        {
                            rowData.Add(viewSchedule.GetCellText(SectionType.Body, i, j));
                        }
                        scheduleData.Add(rowData);
                    }

                    List<string> columnData = scheduleData[0];
                    scheduleData.RemoveAt(0);

                    DataMapping(columnData, scheduleData);
                }
            }
        }

        public static void DataMapping(List<string> keyData, List<List<string>> valueData)
        {
            List<Dictionary<string, string>> items = new List<Dictionary<string, string>>();

            string prompt = "Key/Value";
            prompt += Environment.NewLine;

            foreach (List<string> list in valueData)
            {
                for (int key = 0, value = 0; key < keyData.Count && value < list.Count; key++, value++)
                {
                    Dictionary<string, string> newItem = new Dictionary<string, string>();

                    string k = keyData[key];
                    string v = list[value];
                    newItem.Add(k, v);
                    items.Add(newItem);
                }
            }

            foreach (Dictionary<string, string> item in items)
            {
                foreach (KeyValuePair<string, string> kvp in item)
                {
                    prompt += "Key: " + kvp.Key + ",Value: " + kvp.Value;
                    prompt += Environment.NewLine;
                }
            }

            Autodesk.Revit.UI.TaskDialog.Show("Revit", prompt);
        }
    }
}
