using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
    public interface IConnect
    {
        // Attach an observer to the subject.
        void Attach(IConnectObserver observer);

        // Detach an observer from the subject.
        void Detach(IConnectObserver observer);

        // Notify all observers about an event.
        void Notify();
    }
}
