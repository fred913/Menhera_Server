using System.Net;
using System.Net.Sockets;
using System.Text;
using 服务器;
using 服务器.GameSDKS;
using 服务器.SQL;

namespace ServerProgram
{
    public class Program
    {
        static List<ClientInfo> clients = new List<ClientInfo>();

        public static void Main (string[] args)
        {
            string[] t = new string[] { "PassWord" };
            string[] t1 = new string[] { "一水" };
            var sQLAction = new SQLAction("Users");
            var us = new Users("Users");
            //API.Print(sQLAction.SelectData("db_Users", t, "UID = 10001"));
            //API.Print(sQLAction.SelectData("db_Users", t, "UID = 10001"));



            //sQLAction.InsertData("db_Users", t, t1);
            // sQLAction.UpdateOrCreateData("db_Users", t, t1, "UID = 10001");
            API.Print(SQLT_Operate.TSQL_Read<string>("db_Users", "UID = 10001", t)[0]);



            StartServer();
        }

        static void StartServer ()
        {

            TcpListener listener = new TcpListener(IPAddress.Any, 822);
            //TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 822);
            listener.Start();
            API.Print("Server started.");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ClientInfo newClient = new ClientInfo(client);

                clients.Add(newClient);

                Thread clientThread = new Thread(() => HandleClient(newClient));
                clientThread.Start();
            }
        }

        static void HandleClient (ClientInfo client)
        {
            NetworkStream stream = client.TcpClient.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = 0;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string response = ProcessRequest(request);
                byte[] data = Encoding.UTF8.GetBytes(response);
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch
                {
                    API.Print("GetClient: " + client.IpAddress);
                    break;

                }
            }

            clients.Remove(client);
            client.TcpClient.Close();
        }

        public static string ProcessRequest (string request)
        {
            //API.Print(request);
            return Analysis.GetReturnMessage(request);
        }
    }

    class ClientInfo
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