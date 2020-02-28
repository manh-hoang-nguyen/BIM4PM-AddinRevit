using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
    public class History
    {
        public string _id { get; set; }

        public DateTime modifiedAt { get; set; }

        public User user { get; set; }

        public bool isFirstCommit { get; set; }

        public bool geometryChange { get; set; }

        public bool parameterChange { get; set; }

        public bool sharedParameterChange { get; set; }
    }
}
