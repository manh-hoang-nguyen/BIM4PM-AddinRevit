namespace ProjectManagement.Commun
{
    using Newtonsoft.Json;
    using ProjectManagement.Models;
    using ProjectManagement.Tools;
    using RestSharp;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AuthProvider : INotifyPropertyChanged
    {
        private AuthProvider()
        {
        }

        public static AuthProvider Instance { get; } = new AuthProvider();

        public User CurrentUser { get; set; }

        public Token token { get; set; }

        private bool isAuthenticated;

        private bool _isConnected;

        public bool IsAuthenticated
        {
            get => isAuthenticated; set { isAuthenticated = value; OnAuthenticated(); }
        }

        public bool IsConnected
        {
            get => _isConnected; set { _isConnected = value; OnPropertyChanged(); }
        }

        public void Logout()
        {
            IsAuthenticated = false;
            token = null;
            IsConnected = false; 
            PaletteViewModel.TabItems.RemoveAt(0);
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangedEventHandler AuthenticationChanged;

        public void OnAuthenticationChanged([CallerMemberName] string propertyName = null)
        {
            AuthenticationChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnAuthenticated([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            new Thread(() => PaletteUtilities.LaunchCommunicator())
            {
                Priority = ThreadPriority.BelowNormal,
                IsBackground = true
            }.Start();

            if (IsAuthenticated == true)
            {
                RestRequest req = new RestRequest(Route.GetMe, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

                IRestResponse res = Route.Client.Execute(req);

                UserRes User = JsonConvert.DeserializeObject<UserRes>(res.Content);

                CurrentUser = User.data;
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
