using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdHidePanel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string tabName = "BIMNG-Project Management";
            string panelName = "Project Management";
            Utils.PanelAvailability.Hide(tabName, panelName);
            return Result.Succeeded;
        }
    }
}