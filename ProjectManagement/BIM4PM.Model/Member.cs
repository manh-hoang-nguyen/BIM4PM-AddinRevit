using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
    public class Member
    {
        public string _id { get; set; }
        public User user { get; set; }
        public string role { get; set; }
    }
}
