namespace BIM4PM.UI.CmdRevit
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Newtonsoft.Json;
    using BIM4PM.UI.Commun;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Data;
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
 
            FilteredElementCollector col
            = new FilteredElementCollector(doc)
            .OfClass(typeof(ViewSchedule));

            ViewScheduleExportOptions opt = new ViewScheduleExportOptions();
            string path = Path.GetTempPath();
            string name = "";
            string namex="";
            foreach (ViewSchedule vs in col)
            {
                name = vs.Name +".txt";
                namex = vs.Name;
                vs.Export(path, name, opt);
                break;
            }

            string filename = Path.Combine(path,name);
            BIM4PM.UI.Utils.RevitUtils.ScheduleUtil.ScheduleDataParser parser = new BIM4PM.UI.Utils.RevitUtils.ScheduleUtil.ScheduleDataParser(filename);
            var table = parser.Table;

            
          
            List<string> list = new List<string>();
            for (int i = 1; i < parser.Table.Rows.Count; i++)
            {
                string itemChild = "";
                string item = "";
                for (int j = 0; j < parser.Table.Columns.Count; j++)
                {
                    if(j != parser.Table.Columns.Count - 1)
                    {
                        itemChild += "\"" + parser.Table.Columns[j].ColumnName + "\":\"" + parser.Table.Rows[i][j] + "\",";
                    }
                    else
                    {
                        itemChild += "\"" + parser.Table.Columns[j].ColumnName + "\":\"" + parser.Table.Rows[i][j] + "\"";
                    }
                }
                item = "{" + itemChild + "}";
                list.Add(item);
            }
            string bodychild="[";
            for (int i = 0; i < list.Count; i++)
            {
                if(i != list.Count - 1)
                {
                    bodychild += list[i] +",";
                }
                else
                {
                    bodychild += list[i] + "]";
                }
            }
            string body ="{\"name\":\"" + namex +"\",\""+"isShared\":\"true\"," + "\"data\":"   + bodychild + "}";

            string url = string.Format("{0}/{1}/schedules", Route.UserProjects, ProjectProvider.Instance.CurrentProject._id);
            RestRequest req = new RestRequest(url, Method.POST);
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
            req.RequestFormat = DataFormat.Json;
            req.AddJsonBody(body);
            IRestResponse res = Route.Client.Execute(req);
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            File.Delete(@filename);

            return Result.Succeeded;
        }

        /// <summary>
        /// The getScheduleData
        /// </summary>
        /// <param name="doc">The doc<see cref="Document"/></param>
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

        /// <summary>
        /// The DataMapping
        /// </summary>
        /// <param name="keyData">The keyData<see cref="List{string}"/></param>
        /// <param name="valueData">The valueData<see cref="List{List{string}}"/></param>
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
