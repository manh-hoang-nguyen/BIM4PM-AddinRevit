using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjectManagement.FormInterface;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdModificationWatcher : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            frm_ModificationWatcher frm_ModificationWatcher = new frm_ModificationWatcher();
            frm_ModificationWatcher.Show();
            return Result.Succeeded;
        }
    }
}