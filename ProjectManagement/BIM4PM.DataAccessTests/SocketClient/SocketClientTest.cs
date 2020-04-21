using BIM4PM.DataAccess;
using BIM4PM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Socket= BIM4PM.DataAccess.SocketClient;
namespace BIM4PM.DataAccessTests.SocketClient
{
  public  class SocketClientTest
    {
        public SocketClientTest()
        {
            var authenticationRepository = new AuthenticationRepository();

            var isAuth = authenticationRepository.Login("nguyenhoang56ksgt@gmail.com", "12345678");
        }

        [Fact]
        public void ShouldConnectSocket()
        {
            var socket = new Socket.SocketClient();
            //Thread t = new Thread(() =>
            //{
                socket.ConnectToProject(new Project { Id = "123" });
            //});
            //t.Start();
          
        }
    }
}
