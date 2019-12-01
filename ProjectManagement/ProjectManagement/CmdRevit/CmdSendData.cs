using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using ProjectManagement.Controllers;
using ProjectManagement.FormInterface;
using ProjectManagement.Models;
using System.Collections.Generic;
using System.Threading;
using ProjectManagement.Commun;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSendData : IExternalCommand
    {
        public static string version;
        public static string auteur;
        public static string comment;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
           
          
            List<Comparison> data = new List<Comparison>();
            frm_SendData dialog = new frm_SendData();
            dialog.ShowDialog();

            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            { /*
                Utils.Compare.GetListGuidElement();
                JsonPost.PostComparison = new List<JsonToPostComparison>();
                foreach (string id in GuidList.guid_newElement)
                {
                    Element e = frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e);

                    JsonPost.PostComparison.Add(comparison);

                }
                foreach (string id in GuidList.guid_modifiedElement)
                {
                    Element e = frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e); 
                    JsonPost.PostComparison.Add(comparison);

                }
                #region Modification watcher
                foreach (ElementId id in ModificationTracker.modifiedElement)
                {
                   Element e =frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPostFromDocumentChanged(e);

                    JsonPost.PostComparison.Add(comparison);

                }
                foreach (ElementId id in ModificationTracker.newElement)
                {
                    Element e = frm_Login._doc.GetElement(id);
                    JsonToPostComparison comparison = ComparisonController.CreateJsonPostFromDocumentChanged(e);

                    JsonPost.PostComparison.Add(comparison);

                }
                #endregion
                var convertedJson = JsonConvert.SerializeObject(JsonPost.PostComparison, Formatting.Indented);
                string DATA = "{\"version\":\"" + version + "\","
                            + "\"auteur\":\"" + auteur + "\","
                            + "\"comment\":\"" + comment + "\","
                            + "\"data\":" + convertedJson
                            + "}";
               

               Thread thread = new Thread(() => ComparisonController.PostComparison(DATA));
                thread.Start();

                Initialize(); //Empty list element changed
                */
                return Result.Succeeded;
            }
            else return Result.Cancelled;
        }
        void Initialize()
        {
            
            ModificationTracker.deletedElement= new List<ElementId>();
            ModificationTracker.newElement = new List<ElementId>();
            
            ModificationTracker.modifiedElement = new List<ElementId>();
            GuidList.guid_newElement= new List<string>();
            GuidList.guid_modifiedElement = new List<string>();
            GuidList.guid_deletedElement= new List<string>();
            GuidList.guid_sameElement = new List<string>();
        }
    }
}