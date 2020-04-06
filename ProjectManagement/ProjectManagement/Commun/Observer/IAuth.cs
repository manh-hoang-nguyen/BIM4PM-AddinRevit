using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public interface IAuth
    {
        // Attach an observer to the subject.
        void Attach(IAuthObserver observer);

        // Detach an observer from the subject.
        void Detach(IAuthObserver observer);

        // Notify all observers about an event.
        void Notify();
    }
}
