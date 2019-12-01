using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ProjectManagement.Commun
{
   public  class ModificationTracker 
    {
       public static string ProjectName;
        public static ObservableCollection<ElementId> ObsDeletedElement= new ObservableCollection<ElementId>();
        public static List<ElementId> deletedElement = new List<ElementId>();
        public static List<ElementId> newElement = new List<ElementId>();
        public static List<ElementId> modifiedElement = new List<ElementId>();

       
 
        

       
    }
}
