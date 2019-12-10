using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using ProjectManagement.FormInterface;
using ProjectManagement.Models;
using ProjectManagement.Tools.Auth;
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
            AuthProvider.Instance.Logout();

            return Result.Succeeded;
        }
    }
}
