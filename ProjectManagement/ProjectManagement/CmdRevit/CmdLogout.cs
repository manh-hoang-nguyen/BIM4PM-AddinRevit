using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BIM4PM.UI.Commun;
using System;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdLogout : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //if (AuthProvider.Instance.IsAuthenticated == false)
            //{
            //    DockablePaneId dpid = new DockablePaneId(new Guid(Properties.Resources.PaletteGuid));

            //    DockablePane dp = commandData.Application.GetDockablePane(dpid);

            //    dp.Hide();

            //}

           
              
            return Result.Succeeded;
        }
    }
}