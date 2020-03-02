namespace ProjectManagement.Commun
{
    using Newtonsoft.Json;
    using ProjectManagement.Models;
    using ProjectManagement.Tools;
    using ProjectManagement.Tools.Discussion;
    using ProjectManagement.Tools.History;
    using RestSharp;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
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
        public User CurrentUser { get; set; }

        public Token token { get; set; }

        private bool isAuthenticated ;

        public bool IsAuthenticated
        {
            get => isAuthenticated; set { isAuthenticated = value; OnAuthenticated(); }
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
            DiscussionProvider.Instance.Reset();
            IsConnected = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnAuthenticated([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            new Thread(() => PaletteUtilities.LaunchCommunicator())
            {
                Priority = ThreadPriority.BelowNormal,
                IsBackground = true
            }.Start();

            if(IsAuthenticated == true)
            {
                RestRequest req = new RestRequest(Route.GetMe, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

                IRestResponse res = Route.Client.Execute(req);

                UserRes User = JsonConvert.DeserializeObject<UserRes>(res.Content);

                CurrentUser = User.data;
            }
           
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
                PaletteViewModel.TabItems.Add(new TabItem
                {
                    Content = new DiscussionView() { DataContext = new DiscussionViewModel() },
                    Header = "Discussion"
                });
            }
            else
            {
                int count = PaletteViewModel.TabItems.Count;
                for (int i = 1; i < count; i++)
                {
                    PaletteViewModel.TabItems.RemoveAt(1);
                    
                }
            }
        }
    }
}
