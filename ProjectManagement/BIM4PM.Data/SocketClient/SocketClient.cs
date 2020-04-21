using System;
using System.Collections.Generic;
using System.Linq;
using Quobject.EngineIoClientDotNet.Client.Transports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quobject.Collections.Immutable;
using  Quobject.SocketIoClientDotNet.Client;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using BIM4PM.Model;

namespace BIM4PM.DataAccess.SocketClient
{
   public class SocketClient
    {
        
        private  Socket _socket;
     
        private string BaseUrlLocal = RouteBase.BaseUrl;
        public SocketClient()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", AuthenticationRepository.Token.AccessToken);
            var options = new IO.Options
            {
                IgnoreServerCertificateValidation = true,
                //AutoConnect = true,
                //ForceNew = true,
                //Upgrade = false,
                ExtraHeaders = headers
            };

            //options.Transports = ImmutableList.Create<string>(WebSocket.NAME);
            
            _socket = IO.Socket(BaseUrlLocal, options);
            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                
            });

            _socket.On("synchronization",(body) => {
                var data = JObject.FromObject(body);
                MessageBox.Show(data.Property("_id").ToString());
            });
             
           
            _socket.Emit("synchronization", JObject.FromObject( new Synchronization() { Id = "123" }));
        }
        
        public void ConnectToProject(Project project)
        {
            _socket.Emit("joinProject", JObject.FromObject(project));
            _socket.Disconnect();
        }
        
    }
    
}
