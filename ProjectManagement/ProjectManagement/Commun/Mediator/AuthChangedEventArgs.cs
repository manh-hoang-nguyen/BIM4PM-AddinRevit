using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
  public  class AuthChangedEventArgs:EventArgs
    {
        public AuthProvider AuthProvider { get; set; }
    }
}
