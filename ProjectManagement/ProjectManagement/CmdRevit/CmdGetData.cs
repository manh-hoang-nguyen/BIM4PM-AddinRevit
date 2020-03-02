using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdGetData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            /*
            GuidList.guid_newElement = new List<string>();
            GuidList.guid_deletedElement = new List<string>();
            GuidList.guid_modifiedElement= new List<string>();
            GuidList.guid_sameElement = new List<string>();
            HistoryList.HistoryInDatabase = new List<History>();
            HistoryList.CommentInDatabase = new List<Comment>();

            UIApplication ui_app = commandData.Application;
            UIDocument uiDoc = ui_app.ActiveUIDocument;
            Document doc = uiDoc.Document;
            List<Level> lst_Levels = MethodeRevitApi.GetLevels(doc);

            ComparisonList.ComparisonInDatabase = ComparisonController.GetComparison();

            HistoryList.HistoryInDatabase=  HistoryController.GetHistory();

            Thread thread = new Thread(getComment);
            thread.Start();

            VersionList.VersionInDatabase= VersionController.GetVersion();
            ComparisonList.ComparisonInModel = new List<Comparison>();
            foreach (Element e in MethodeRevitApi.GetElementOfCategories(doc))
            {
                ComparisonList.ComparisonInModel.Add(ComparisonController.CreateComparison(e));
            }

            Utils.Compare.GetListGuidElement();
            JsonPost.PostComparison = new List<JsonToPostComparison>();
            foreach (Element e in MethodeRevitApi.GetElementOfCategories(doc))
            {
                JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e);

                JsonPost.PostComparison.Add(comparison);
            }
            */
            /*  DataList.DataModel = new List<Data>();
              foreach (Element e in MethodeRevitApi.GetElementOfCategories(doc))
              {
                  Data data = DataController.CreateData(e, lst_Levels);

                  DataList.DataModel.Add(data);
              }
              DataList.DataInDatabase =  DataController.GetData();
              IEnumerable<Data> xx = from e in DataList.DataInDatabase
                                     where e.status == "modified"
                                     select e;
                                 */
            return Result.Succeeded;
        }

        private void getComment()
        {
        }
    }
}