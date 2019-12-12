using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Routes;
using System;
using ProjectManagement.Models;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdSynchronization : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            HistoryList.HistoryInDatabase = new List<Historyx>();
            HistoryList.CommentInDatabase = new List<Comment>();
            ComparisonList.ComparisonInDatabase = ComparisonController.GetComparison();

            HistoryList.HistoryInDatabase = HistoryController.GetHistory();
            foreach (Historyx item in HistoryList.HistoryInDatabase)
            {
                Comment comment = new Comment()
                {
                    

                };
                HistoryList.CommentInDatabase.Add(comment);
            }
            ProjectProvider.Instance.Versions = VersionController.GetVersion();
            return Result.Succeeded;
        }
    }
}
