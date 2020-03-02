using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
   public class Authentication
    {
        public string token { get; set; }
        public string userId { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
