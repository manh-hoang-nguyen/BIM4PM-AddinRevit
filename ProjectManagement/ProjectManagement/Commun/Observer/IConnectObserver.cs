using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public interface IConnectObserver
    {
        void Update(IConnect connect);
    }
}
