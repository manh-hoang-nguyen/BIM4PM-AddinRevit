using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Models
{
   public class Authentication
    {
        public string token { get; set; }
        public string userId { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
