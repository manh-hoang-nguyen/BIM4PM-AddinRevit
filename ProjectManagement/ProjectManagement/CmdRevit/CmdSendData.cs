using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using ProjectManagement.Models;
using GraphQL.Client.Http;

namespace ProjectManagement.CmdRevit
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