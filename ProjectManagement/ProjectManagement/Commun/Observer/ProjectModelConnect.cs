using Autodesk.Revit.DB;
using BIM4PM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIM4PM.UI.Commun
{
    public class ProjectModelConnect : IConnect, IAuthObserver
    {
        public static Project SelectedProject { get; private set; }
        public static Document SelectedRevitModel { get;private set; }
        public static bool IsConnected { get; private set; }
        private List<IConnectObserver> _observers = new List<IConnectObserver>();
        public ProjectModelConnect(Project project, Document model)
        {
            SelectedProject = project;
            SelectedRevitModel = model;
        }

        public void Attach(IConnectObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Detach(IConnectObserver observer)
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

        public void Connect()
        {
            IsConnected = true;
            this.Notify();
        }

        public void Disconnect()
        {
            IsConnected = false;
            SelectedProject = null;
            SelectedProject = null;
            this.Notify();
        }

        public void Update(IAuth subject)
        {
            if(Auth.IsAuthenticated == false)
            {
                Disconnect();
            }
        }
    }
}
