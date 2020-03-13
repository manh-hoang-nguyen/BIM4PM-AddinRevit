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
            get => isAuthenticated; set { isAuthenticated = value; OnPropertyChanged("IsAuthenticated"); }
        }

        public bool IsConnected
        {
            get => _isConnected; set { _isConnected = value; OnPropertyChanged("IsConnected"); }
        }

        public void Logout()
        {
            IsAuthenticated = false;
            token = null;
            IsConnected = false; 
           
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        
       
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (IsAuthenticated == true)
            {

                new Thread(() => PaletteUtilities.LaunchCommunicator())
                {
                    Priority = ThreadPriority.BelowNormal,
                    IsBackground = true
                }.Start();

                RestRequest req = new RestRequest(Route.GetMe, Method.GET);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Authorization", "Bearer " + AuthProvider.Instance.token.token);

                IRestResponse res = Route.Client.Execute(req);

                UserRes User = JsonConvert.DeserializeObject<UserRes>(res.Content);

                CurrentUser = User.data;
            }
        }
    }
}
