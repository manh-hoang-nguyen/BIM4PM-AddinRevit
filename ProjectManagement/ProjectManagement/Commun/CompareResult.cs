using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
   public class CompareResult
    {
        public static IEnumerable<RevitElement> DeletedElements;
        public static IEnumerable<RevitElement> ModifiedElements;
        public static IEnumerable<RevitElement> NewElements;
        public static IEnumerable<RevitElement> SameElements; 
    }
}
