using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using ProjectManagement.FormInterface;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public  class CmdLogout:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string tabName = "BIMNG-Project Management";
            Utils.PanelAvailability.HideAll(tabName);

            HistoryList.HistoryInDatabase = new List<History>();
            HistoryList.CommentInDatabase = new List<Comment>();
          
            ModificationTracker.deletedElement = new List<ElementId>();
            ModificationTracker.newElement = new List<ElementId>();
            ModificationTracker.modifiedElement = new List<ElementId>();
            UserData.authentication.token = null;
            UserData.authentication.userId = null;
            VersionCommun.CurrentVersion = null;
            VersionCommun.VersionInDatabase = null;
            ComparisonList.ComparisonInDatabase= new List<Comparison>();
            ComparisonList.ComparisonInModel = new List<Comparison>();
            GuidList.guidInModel= new List<string>();
            GuidList.guid_deletedElement = new List<string>();
            GuidList.guid_ElementToExamine = new List<string>();
            GuidList.guid_modifiedElement = new List<string>();
            GuidList.guid_newElement = new List<string>();
            JsonPost.PostComparison_ModifiedElement = null;
            JsonPost.PostComparison_NewElement = null;
            DataList.DataInDatabase = new List<Data>();
            #region unsubcribe event
            App._uicapp.ControlledApplication.DocumentChanged -= new EventHandler<DocumentChangedEventArgs>(frm_Login.OnDocumentChanged);
            foreach (Autodesk.Windows.RibbonTab tab in Autodesk.Windows.ComponentManager.Ribbon.Tabs)
            {
                if (tab.Id == "Modify")
                {
                    tab.PropertyChanged -= PanelProprety.PanelEvent;
                   
                }
            }
            App.Instance.TextChangedButton();
            #endregion

            return Result.Succeeded;
        }
    }
}
