using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.CmdRevit.Utils
{
     
        public class AvailabilityButton : IExternalCommandAvailability
        {
            public bool IsCommandAvailable(  UIApplication a, CategorySet b)
            {
                return true;
            }
        }
    public class DesactiveAvailabilityButton : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication a, CategorySet b)
        {
            return false;
        }
    }
}
