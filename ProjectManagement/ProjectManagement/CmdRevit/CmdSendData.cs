using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
 
using System.Threading.Tasks;
 
using System;
using BIM4PM.UI.Models;
 

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdSendData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
          
           
            return Result.Succeeded;
        }
 
 
    }
}