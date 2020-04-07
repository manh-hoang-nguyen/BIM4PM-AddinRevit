using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System.Collections.Generic;

namespace BIM4PM.UI.CmdRevit
{
    [Transaction(TransactionMode.ReadOnly)]
    public class CmdGetData : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            

            return Result.Succeeded;
        }



    }
    public class PersonAndFilmsResponse
    {
        public PersonContent Person { get; set; }

        public class PersonContent
        {
            public string Name { get; set; }
            public FilmConnectionContent FilmConnection { get; set; }

            public class FilmConnectionContent
            {
                public List<FilmContent> Films { get; set; }

                public class FilmContent
                {
                    public string Title { get; set; }
                }
            }
        }
    }
}