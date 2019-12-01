using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Routes
{
  public  class ProjectRouter
    {
        /// <summary>
        /// json: projectName, userMail, comment
        /// </summary>
        public static string Project = "https://manh-hoang.herokuapp.com/api/project";
        /// <summary>
        /// json GET: projectId, jsonPOST:projectId, version, auteur, comment
        /// </summary>
        public static string ProjectVersion = "https://manh-hoang.herokuapp.com/api/project/version";
    }
}
