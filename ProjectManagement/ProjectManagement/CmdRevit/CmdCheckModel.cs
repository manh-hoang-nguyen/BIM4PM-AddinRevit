using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdCheckModel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication ui_app = commandData.Application;

            return Result.Succeeded;
        }
    }
}