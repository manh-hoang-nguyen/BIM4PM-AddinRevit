namespace ProjectManagement.Commun
{
    using ProjectManagement.Models;
    using ProjectManagement.Tools;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AuthProvider : INotifyPropertyChanged
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

        public bool IsAuthenticated
        {
            get => isAuthenticated; set { isAuthenticated = value; OnPropertyChanged(); }
        }

        private bool isAuthenticated;
        public void Logout()
        {
            IsAuthenticated = false;
            token = null;
            Disconnect();
        }
        public void Disconnect()
        { 
            ProjectProvider.Instance.Reset();
            ModelProvider.Instance.Reset();
            CompareProvider.Instance.Reset();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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
