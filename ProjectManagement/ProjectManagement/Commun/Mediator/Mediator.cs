using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{ 
   public sealed class Mediator
    {
        private static readonly Mediator _instance = new Mediator();

        private Mediator() { }

        public static Mediator Instance => _instance;

        public event EventHandler<AuthChangedEventArgs> AuthChanged;

        public void OnAuthChanged(object sender, AuthProvider authProvider)
        {
            (AuthChanged as EventHandler<AuthChangedEventArgs>)?.Invoke(sender, new AuthChangedEventArgs {AuthProvider = authProvider});
        }
    }
}
