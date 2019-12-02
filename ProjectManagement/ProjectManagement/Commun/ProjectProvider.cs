using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
   public   class ProjectProvider
    {
        private static ProjectProvider _ins;
        public static ProjectProvider Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ProjectProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        public Project CurrentProject { get => _currentProject; set => _currentProject = value; }

        private Project _currentProject;
      
        public   IList<Project> UserProjects;
         
    }
    
}
