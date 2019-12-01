using System.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using ProjectManagement.FormInterface;
using System.Collections.Generic;
using System;
using ProjectManagement.Utils.RevitUtils;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdTestProgressBar : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //    Thread thread = new Thread(new ThreadStart(() =>
            //    {
            //        // create and show the window
            //        frm_ProgressBar frm_ProgressBar = new frm_ProgressBar();
            //        frm_ProgressBar.Show();

            //        // start the Dispatcher processing  
            //        System.Windows.Threading.Dispatcher.Run();
            //    }));

            //    // set the apartment state  
            //    thread.SetApartmentState(ApartmentState.STA);

            //    // make the thread a background thread  
            //    thread.IsBackground = true;

            //    thread.Start();

            // UIDocument UIdoc = commandData.Application.ActiveUIDocument;
            // Application app = commandData.Application.Application;
            // Document doc = UIdoc.Document;
            // Settings settings = doc.Settings;
            //var xxk= doc.SiteLocation;
            // var l = doc.Phases;
            // var x = doc.ProjectLocations;
            // string xx = "";
            // //foreach (Category item in categories)
            // //{

            // //    xx += item.Name +" "+ item.Parent + Environment.NewLine;
            // //}
            // //TaskDialog.Show("revit", xx);
            // Reference reference = UIdoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);
            // Element element = UIdoc.Document.GetElement(reference);
            //// ProjectManagement.Utils.RevitUtils.ParameterUtils.GetAllParametersElement(element);
            // //foreach (KeyValuePair<string, string> entry in ProjectManagement.Utils.RevitUtils.ParameterUtils.GetAllParametersElement(element))
            // //{
            // //    xx += entry.Key + " : " + entry.Value + Environment.NewLine;
            // //}
            // foreach(Parameter p in element.GetOrderedParameters())
            // {
            //    if( p.IsShared)
            //     xx += p.Definition.Name +" : "+ ParameterUtils.ParameterToString(p)  + Environment.NewLine;
            // }
            // TaskDialog.Show("revit", xx);
            // double ind = 12;

            TaskDialog.Show("KK", "KKKK");


            return Result.Succeeded;
        }
        void Progre()
        {

        }
    }
}

