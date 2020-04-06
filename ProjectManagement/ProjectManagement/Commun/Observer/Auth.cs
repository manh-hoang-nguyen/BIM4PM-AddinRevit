using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIM4PM.DataAccess;
namespace BIM4PM.UI.Commun
{
    public class Auth : IAuth
    {
         
        public static bool IsAuthenticated { get; private set; }

        private IAuthenticationRepository _authenticationRepository;

        public Auth()
        {
            _authenticationRepository = new AuthenticationRepository();
        }
        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        private List<IAuthObserver> _observers = new List<IAuthObserver>();

        public void Attach(IAuthObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Detach(IAuthObserver observer)
        {
            this._observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void Login(string email, string password)
        {
            IsAuthenticated = _authenticationRepository.Login(email, password).Item1;
             
            this.Notify();
        }
        public void Logout()
        {
            IsAuthenticated = false;

            this.Notify();
        }
    }
}
