using BIM4PM.UI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
    public class LaunchPanel : IAuthObserver
    {
        public void Update(IAuth subject)
        {
            if(Auth.IsAuthenticated)
            {
                new Thread(() => PaletteUtilities.LaunchPanel())
                {
                    Priority = ThreadPriority.BelowNormal,
                    IsBackground = true
                }.Start();
            }
            else
            {
                new Thread(() => PaletteUtilities.ClearPanel())
                {
                    Priority = ThreadPriority.BelowNormal,
                    IsBackground = true
                }.Start(); 
            }
        }

        
    }
}
