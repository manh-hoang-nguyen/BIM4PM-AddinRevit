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
        public static IEnumerable<string> DeletedElements;
        public static IEnumerable<string> ModifiedElements;
        public static IEnumerable<string> NewElements;
        public static IEnumerable<string> SameElements; 
    }
}
