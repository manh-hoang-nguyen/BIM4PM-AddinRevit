using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    partial class CmdSendDeletedElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Compare.DeletedElement();
            //foreach(string guid in DataList.guid_deletedElement)
            //{
            //    string json = "{\"guid\":\"" + guid + "\"}";
            //    DataController.ChangeStatusDeletedElement(json);
            //}

            return Result.Succeeded;
        }
    }
}