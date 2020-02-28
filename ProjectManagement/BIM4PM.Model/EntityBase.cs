using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.Model
{
   public abstract class EntityBase
    {

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        
    }
}
