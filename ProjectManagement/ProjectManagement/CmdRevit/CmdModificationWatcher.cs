using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
 

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdModificationWatcher : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
           
            return Result.Succeeded;
        }
    }
}