namespace ProjectManagement.Tools.History
{
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
    using System.Threading;
    using System.Windows;

    public class HistoryRequestHandler : IExternalEventHandler
    {
        public HistoryRequest Request { get; set; } = new HistoryRequest();

        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="app">The app<see cref="UIApplication"/></param>
        public void Execute(UIApplication app)
        {

            try
            {
                switch (Request.Take())
                {

                    case HistoryRequestId.Refresh:
                        Refresh(app);
                        break;
                    case HistoryRequestId.None:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// The GetName
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetName()
        {
            return "History External Event";
        }

        /// <summary>
        /// The Refresh
        /// </summary>
        /// <param name="app">The app<see cref="UIApplication"/></param>
        private void Refresh(UIApplication app)
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
                    RevitElementRoute route = new RevitElementRoute(ProjectProvider.Instance.CurrentProject._id);
                    Element e = doc.GetElement(selectedIds.First());
                    if ((null != e.Category
                            && 0 < e.Parameters.Size
                            && (e.Category.HasMaterialQuantities)) == false)
                    {
                        MessageBox.Show("This element is NOT SUPPORTED by us. Sorry!");
                        return;
                    }
                     string guid = e.UniqueId;
                    RestRequest req = new RestRequest(route.historyUrl(guid), Method.GET);
                    req.AddHeader("Content-Type", "application/json");
                    req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);
                    IRestResponse<Models.History> res = Route.Client.Execute<Models.History>(req);

                    if(res.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Element is not synchronized on cloud yet.");
                        return;
                    }
                    if(res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string format = "0000-12-31T23:50:39.000Z";
                        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format, Culture = CultureInfo.InvariantCulture };
                        HistoryResParent revitElements = JsonConvert.DeserializeObject<HistoryResParent>(res.Content, dateTimeConverter);

                        HistoryModel.HistoriesByTypeChange.Clear();
                        foreach (HistoryByTypeChange item in GetHistoryByType(revitElements.data.history))
                        {
                            HistoryModel.HistoriesByTypeChange.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Some error on getting element history.");
                        
                    }

                  
                    break;
                default:
                    MessageBox.Show("Please select ONLY an element");
                    break;
            }
        }

        /// <summary>
        /// The GetHistoryByType
        /// </summary>
        /// <param name="Histories">The Histories<see cref="List{Models.History}"/></param>
        /// <returns>The <see cref="List{HistoryByTypeChange}"/></returns>
        private List<HistoryByTypeChange> GetHistoryByType(List<Models.History> Histories)
        {
            List<HistoryByTypeChange> hisByChange = new List<HistoryByTypeChange>();

            foreach (Models.History history in Histories)
            {
                DateTime date = history.modifiedAt;
                string userName = history.user.name.fullName();

                if (history.isFirstCommit == true)
                {
                    HistoryByTypeChange firstCommit = new HistoryByTypeChange()
                    {
                        date = date,
                        userName = userName,
                        type = TypeChange.CreatedOn
                    };
                    hisByChange.Add(firstCommit);
                    continue;
                }

                if (history.geometryChange == true)
                {
                    HistoryByTypeChange geo = new HistoryByTypeChange()
                    {
                        date = date,
                        userName = userName,
                        type = TypeChange.Geometry

                    };
                    hisByChange.Add(geo);
                }
                if (history.parameterChange == true)
                {
                    HistoryByTypeChange para = new HistoryByTypeChange()
                    {
                        date = date,
                        userName = userName,
                        type = TypeChange.Parameters

                    };
                    hisByChange.Add(para);
                }
                if (history.sharedParameterChange == true)
                {
                    HistoryByTypeChange sharedPara = new HistoryByTypeChange()
                    {
                        date = date,
                        userName = userName,
                        type = TypeChange.SharedParameters

                    };
                    hisByChange.Add(sharedPara);
                }
            }

            return hisByChange;
        }
    }

    public class HistoryRequest
    {
        private int _request = (int)HistoryRequestId.None;
         
        public HistoryRequestId Take()
        {
            return (HistoryRequestId)Interlocked.Exchange(ref _request, (int)HistoryRequestId.None);
        }
 
        public void Make(HistoryRequestId request)
        {
            Interlocked.Exchange(ref _request, (int)request);
        }
    }

    public enum HistoryRequestId
    {
        None,
        Refresh
    }
}
