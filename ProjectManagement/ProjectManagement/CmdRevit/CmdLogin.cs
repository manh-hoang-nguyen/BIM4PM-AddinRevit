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
using ProjectManagement.Tools.Auth;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdLogin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            
            /*
            frm_Login frm_Login = new frm_Login(ui_app);
            frm_Login.ShowDialog();
            if (frm_Login.DialogResult.HasValue && frm_Login.DialogResult.Value)
            {
                
                //GET DATA IN DATABASE
                HistoryList.HistoryInDatabase = new List<History>();
                HistoryList.CommentInDatabase = new List<Comment>();
                ComparisonList.ComparisonInDatabase = ComparisonController.GetComparison();
                HistoryList.HistoryInDatabase = HistoryController.GetHistory();
                VersionCommun.VersionInDatabase = VersionController.GetVersion();
                VersionCommun.VersionActuel = VersionCommun.VersionInDatabase.Count;
                HistoryList.CommentInDatabase = CommentController.GetAllCommentInProject(); 

                //Show all panel
                PanelAvailability.ShowAll("BIMNG-Project Management");
                //change text button login
                App.Instance.TextChangedButton();
                
                return Result.Succeeded;
            }
            */
            LoginView loginView = new LoginView()
            {
                DataContext = new LoginViewModel(  uiapp)
            };
            loginView.ShowDialog();
            return Result.Succeeded;
            //else return Result.Cancelled;
        }
    }
}
