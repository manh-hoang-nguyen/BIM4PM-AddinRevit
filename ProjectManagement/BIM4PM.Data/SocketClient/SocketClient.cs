namespace BIM4PM.DataAccess.SocketClient
{
    using BIM4PM.Model;
    using GalaSoft.MvvmLight.Messaging;
    using Newtonsoft.Json.Linq;
    using Quobject.SocketIoClientDotNet.Client;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class SocketClient
    {
        private Socket _socket;

        private string BaseUrlLocal = RouteBase.BaseUrl;

        public SocketClient()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", AuthenticationRepository.Token.AccessToken);
            var options = new IO.Options
            {
                IgnoreServerCertificateValidation = true,
                AutoConnect = true,
                ForceNew = true,
                Upgrade = false,
                ExtraHeaders = headers
            };

            //options.Transports = ImmutableList.Create<string>(WebSocket.NAME);

            _socket = IO.Socket(BaseUrlLocal, options);
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                //MessageBox.Show("Connected");
                //MessageBox.Show("Join");
                Messenger.Default.Register<Project>(this, (action) => MessageBox.Show(action.Name));
            });

            _socket.On("synchronization", (body) =>
            {
                var data = JObject.FromObject(body);
                MessageBox.Show(data.Property("_id").ToString());
              
            });
            _socket.On("joinProject", () =>
            {
                MessageBox.Show("Join");
                Messenger.Default.Register<Project>(this, (action) => MessageBox.Show(action.ToString()));
            });

            _socket.Emit("synchronization", JObject.FromObject(new Synchronization() { Id = "123" }));
        }

        public void ConnectToProject(Project project)
        {
            var meassage = new NotificationMessage(project, "jointproect");

            Messenger.Default.Send<Project>(project);
            _socket.Emit("joinProject", JObject.FromObject(project));
        }
    }
}
