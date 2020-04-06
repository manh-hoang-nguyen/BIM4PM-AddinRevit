﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdGetComment : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //CommentList.CommentInDatabase = CommentController.GetData();
            return Result.Succeeded;
        }
    }
}