using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Routes
{
    public class ComparisonRouter
    {
        /// <summary>
        /// json: projectId
        /// </summary>
        public static string GetComparison = "https://manh-hoang.herokuapp.com/api/comparison";
        /// <summary>
        /// json: projectId, version, data[]
        /// </summary>
        public static string PostModifiedElement = "https://manh-hoang.herokuapp.com/api/comparison/modified-element";

        /// <summary>
        /// json: projectId, version, data[]
        /// </summary>
        public static string PostNewElement = "https://manh-hoang.herokuapp.com/api/comparison/new-element";


        /// <summary>
        /// json: projectId, data[]
        /// </summary>
        public static string PostDeletedElement = "https://manh-hoang.herokuapp.com/api/comparison/deleted-element";

    }
}
