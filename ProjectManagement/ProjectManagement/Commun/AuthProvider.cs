namespace ProjectManagement.Commun
{
    using ProjectManagement.Models;
    using ProjectManagement.Tools;
    using ProjectManagement.Tools.History;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Controls;

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

        private bool isAuthenticated;

        public bool IsAuthenticated
        {
            get => isAuthenticated; set { isAuthenticated = value; OnPropertyChanged(); }
        }

        public bool IsConnected { get => isConnected; set { isConnected = value; OnConnected(); } }

        private bool isConnected;

        public void Logout()
        {
            IsAuthenticated = false;
            token = null;
            IsConnected = false;
            Disconnect();
            PaletteViewModel.TabItems.RemoveAt(0);
             
        }
        public void Connect()
        {
            IsConnected = true;
        }
        public void Disconnect()
        { 
            ProjectProvider.Instance.Reset();
            ModelProvider.Instance.Reset();
            CompareProvider.Instance.Reset();
            IsConnected = false;
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
        protected virtual void OnConnected([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if(IsConnected== true)
            {
                PaletteViewModel.TabItems.Add(new TabItem
                {
                    Content = new HistoryView() { DataContext = new HistoryViewModel() },
                    Header = "History"
                });
            }
            else
            {
                for (int i = 1; i < PaletteViewModel.TabItems.Count; i++)
                {
                    PaletteViewModel.TabItems.RemoveAt(i);
                }
            }
        }
    }
}
