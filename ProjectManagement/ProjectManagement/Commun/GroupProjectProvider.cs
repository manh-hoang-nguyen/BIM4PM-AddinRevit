using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public class GroupProjectProvider
    {
        public static List<GroupProject> GroupProjects { get; set; }
        public static GroupProject CurrentGroupProject { get; set; }
    }
}
