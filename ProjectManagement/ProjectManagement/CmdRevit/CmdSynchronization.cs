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
            HistoryList.HistoryInDatabase = new List<History>();
            HistoryList.CommentInDatabase = new List<Comment>();
            ComparisonList.ComparisonInDatabase = ComparisonController.GetComparison();

            HistoryList.HistoryInDatabase = HistoryController.GetHistory();
            foreach (History item in HistoryList.HistoryInDatabase)
            {
                Comment comment = new Comment()
                {
                    guid = item.guid,
                    comments = item.comments.ToList()

                };
                HistoryList.CommentInDatabase.Add(comment);
            }
            VersionCommun.VersionInDatabase = VersionController.GetVersion();
            return Result.Succeeded;
        }
    }
}
