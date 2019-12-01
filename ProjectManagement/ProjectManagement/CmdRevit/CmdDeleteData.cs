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

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdDeleteData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var t1 = Task.Run(() => HttpAysnc.DeleteURI(new Uri(ComparisonRouter.GetComparison)));
            var t2 = Task.Run(() => HttpAysnc.DeleteURI(new Uri( VersionRouter.GetVersion)));
            var t3 = Task.Run(() => HttpAysnc.DeleteURI(new Uri(HistoryRouter.GetHistory)));

           
            return Result.Succeeded;
        }
    }
}
