using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
   public class EventProvider
    {
        
        private EventProvider() { }
        public static EventProvider Instance { get; } = new EventProvider();
        public IEventAggregator EventAggregator { get; private set; } = new EventAggregator();

    }
}
