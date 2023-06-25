using System.Net;
using System.Net.Sockets;

namespace TCP_Server.TCPClient
{
    public class ClientInfo
    {
        public TcpClient TcpClient { get; set; }
        public string ClientId { get; set; }
        public string IpAddress { get; set; }

        public ClientInfo (TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            ClientId = Guid.NewGuid().ToString();
            IpAddress = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
        }
    }
}
