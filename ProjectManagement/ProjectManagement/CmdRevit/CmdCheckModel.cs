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
using ProjectManagement.CmdRevit.Utils;
using System.Linq;
using System;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdCheckModel : IExternalCommand
    {
         
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication ui_app = commandData.Application;
            //GET DATA MODEL
            Document _doc = MethodeRevitApi.GetDocumentByTitle(ui_app, ModificationTracker.ProjectName);
            ComparisonList.ComparisonInModel = new List<Comparison>();
            foreach (Element ele in MethodeRevitApi.GetElementOfCategories(_doc))
            {
                ComparisonList.ComparisonInModel.Add(ComparisonController.CreateComparison(ele)); 
            }

            //GET COMPARE DATA
            GuidList.guid_modifiedElement = new List<string>();
            GuidList.guid_sameElement = new List<string>();
            Compare.GetListGuidElement();
            foreach (string guid in GuidList.guid_ElementToExamine)
            {
                bool comparison;
                Comparison e1 = (from e in ComparisonList.ComparisonInDatabase
                                 where e.guid == guid
                                 select e).FirstOrDefault();
                Comparison e2 = (from e in ComparisonList.ComparisonInModel
                                 where e.guid == guid
                                 select e).FirstOrDefault();
                comparison = Compare.CompareElements(e1, e2);
                if (comparison == true)
                {
                    GuidList.guid_sameElement.Add(guid);
                }
                else GuidList.guid_modifiedElement.Add(guid);
            }

             
            if ((GuidList.guid_deletedElement.Count() != 0 )
                || GuidList.guid_newElement.Count() !=0
                || GuidList.guid_modifiedElement.Count != 0)
            {
                TaskDialog.Show("Result","Nous avons trouvé: " + Environment.NewLine
                                + GuidList.guid_modifiedElement.Count.ToString() + " éléments modifiés" + Environment.NewLine
                                +GuidList.guid_newElement.Count().ToString() + " éléments nouveaux" + Environment.NewLine
                                +GuidList.guid_deletedElement.Count().ToString() + " éléments supprimés");

                TaskDialog taskDialog = new TaskDialog("Result");
                taskDialog.MainContent = "Voulez-vous mettre à jour les données sur cloud maintenant?";
                TaskDialogCommonButtons buttons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;
                taskDialog.CommonButtons = buttons;
                TaskDialogResult taskDialogResult = taskDialog.Show();
                if (taskDialogResult == TaskDialogResult.Yes)
                {
                    frm_UpdateDataInCloud frm_UpdateDataInCloud = new frm_UpdateDataInCloud();

                    frm_UpdateDataInCloud.ShowDialog();
                    int count = GuidList.guid_deletedElement.Count() + GuidList.guid_newElement.Count() + GuidList.guid_modifiedElement.Count;

                    if (frm_UpdateDataInCloud.DialogResult.HasValue && frm_UpdateDataInCloud.DialogResult.Value)
                    {
                        string comment = frm_UpdateDataInCloud.comment.Text;
                        Thread thread = new Thread(new ThreadStart(() =>
                        {
                            // create and show the window
                            frm_ProgressBar frm_ProgressBar = new frm_ProgressBar(count,comment);
                            frm_ProgressBar.Show();
                            //foreach (string id in GuidList.guid_newElement)
                            //{
                            //    Element e = frm_Login._doc.GetElement(id);
                            //    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e, comment, UserData.user.userName);
                            //    string jsonNew = JsonConvert.SerializeObject(comparison, Formatting.Indented);
                               
                            //    string jsNewx = "{\"projectId\":\"" + UserData.idProjectActive
                            //           + "\",\"version\":\"" + VersionCommun.VersionActuel
                            //           + "\",\"data\":" + jsonNew + "}";
                            //    ComparisonController.PostComparison_NewElement(jsNewx);
                            //    frm_ProgressBar.WorkerState += 100 / count;
                            //    JsonPost.PostComparison_NewElement.Add(comparison);

                            //}
                            //foreach (string id in GuidList.guid_modifiedElement)
                            //{
                            //    Element e = frm_Login._doc.GetElement(id);
                            //    JsonToPostComparison comparison = ComparisonController.CreateJsonPost(e, comment, UserData.user.userName);
                            //    string jsonModif = JsonConvert.SerializeObject(comparison, Formatting.Indented);
                            //    frm_ProgressBar.WorkerState += 100 / count;
                            //    JsonPost.PostComparison_ModifiedElement.Add(comparison);

                            //}
                            // start the Dispatcher processing  
                            System.Windows.Threading.Dispatcher.Run();
                           
                        }));

                        // set the apartment state  
                        thread.SetApartmentState(ApartmentState.STA);

                        // make the thread a background thread  
                        thread.IsBackground = true;

                        thread.Start();
                      
                        var convertedJson_new = JsonConvert.SerializeObject(JsonPost.PostComparison_NewElement, Formatting.Indented);
                        var convertedJson_modif = JsonConvert.SerializeObject(JsonPost.PostComparison_ModifiedElement, Formatting.Indented);
                        var convertedJson_delete = JsonConvert.SerializeObject(GuidList.guid_deletedElement, Formatting.Indented);
                        //Send data to server
                        string jsNew = "{\"projectId\":\""+UserData.idProjectActive
                                        +"\",\"version\":\""+ ProjectProvider.Instance.CurrentVersion.version 
                                        +"\",\"data\":" +convertedJson_new+"}";
                        string jsModif = "{\"projectId\":\"" + UserData.idProjectActive
                                        + "\",\"version\":\"" + ProjectProvider.Instance.CurrentVersion.version
                                        + "\",\"data\":" + convertedJson_modif + "}";

                        string jsDel = "{\"projectId\":\"" + UserData.idProjectActive + "\",\"data\":"+convertedJson_delete+"}";

                        //Thread thread1 = new Thread(() => ComparisonController.PostComparison_ModifElement(convertedJson_modif));
                        //thread1.Start();
                        //Thread thread2 = new Thread(() => ComparisonController.PostComparison_NewElement(jsNew));
                        //thread2.Start();
                        //Thread thread3 = new Thread(() => ComparisonController.PostComparison_NewElement(jsDel));
                        //thread3.Start();
                    }
                }
                else
                {
                    return Result.Cancelled;
                }
            }
            else
            {
                TaskDialog.Show("Result", "Les données sont à jour sur cloud");
            }

            return Result.Succeeded;
        }
    }
}
