using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using ProjectManagement.Models;
using ProjectManagement.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Commun
{
   public class AuthProvider:INotifyPropertyChanged
    {
        private static AuthProvider _ins;
        public static AuthProvider Instance
        {
            get
            {
                if (_ins == null)
                    _ins = new AuthProvider();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }
        public Token token { get; set; }
        public bool IsAuthenticated { get => isAuthenticated; set { isAuthenticated = value; OnPropertyChanged(); } }

        private bool isAuthenticated;
        public UIControlledApplication uiapp;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            new Thread(() => PaletteUtilities.LaunchCommunicator())
            {
                Priority = ThreadPriority.BelowNormal,
                IsBackground = true
            }.Start();

             
        }
         
    }
}
