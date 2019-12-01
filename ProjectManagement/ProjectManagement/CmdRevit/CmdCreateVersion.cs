using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ProjectManagement.Commun;
using ProjectManagement.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Controllers.Utils;
using ProjectManagement.Routes;
using System;
using System.Net.Http;
using System.Text;
using ProjectManagement.FormInterface;

namespace ProjectManagement.CmdRevit
{
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateVersion : IExternalCommand
    {
        public static string auteur;
        public static string comment;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            



            frm_CreateVersion dialog = new frm_CreateVersion();
            dialog.ShowDialog();
            if (dialog.DialogResult.HasValue && dialog.DialogResult.Value)
            {


                Uri u = new Uri(VersionRouter.GetVersion);
                var payload = "{\"auteur\":\"" + auteur + "\",\"comment\":\"" + comment + "\"}";

                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");

                var t = Task.Run(() => HttpAysnc.PostURI(u, c));
                t.Wait();



                return Result.Succeeded;
            }
            else return Result.Cancelled;

 
        }
    }
}
