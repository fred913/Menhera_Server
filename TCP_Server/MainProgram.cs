#pragma warning disable CS0164
using SDk;
using SDK;
using SDK.GameSDKS;
using SQL;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TCP_Server.TCPClient;

namespace ServerProgram
{
    public class Program
    {
        public static List<ClientInfo> clients = new List<ClientInfo>();

        public static void Main (string[] args)
        {
            string[] t = new string[] { "PassWord" };
            string[] t1 = new string[] { "一水" };
            var sQLAction = new SQLAction("Users");
            var us = new Users("Users");
            //API.Print(sQLAction.SelectData("db_Users", t, "UID = 10001"));
            //API.Print(sQLAction.SelectData("db_Users", t, "UID = 10001"));

            Test_SQLT_Operate:
            //测试通过
            //SQLT_Operate.TSQL_Update("db_Users", "UID = 10001", API.GetArray<string>("UserName"), API.GetArray<string>("一水"));
            /*测试通过，返回的是数组
            foreach (var item in SQLT_Operate.TSQL_Read<string>("db_Users", "UID = 10001", API.GetArray<string>("UserName")))
            {
                API.Print(item);
            }*/
            Test_User:

            //us.UpdateUserInfo("db_Users", 10001.ToString(), "UserName", "一水久钟");通过

            /*测试通过
        API.Print(Analysis.GetReturnMessage($"UpdateInfo&UID = 10001&f36bb8bcda27e0e0ceb6e4bc3a64a506&UserName&一水久"));
        API.Print(Analysis.GetReturnMessage($"GetInfo&UID = 10001&f36bb8bcda27e0e0ceb6e4bc3a64a506&db_Users&UserName"));
            
            */
            ///API.Getverification("zhangzijian_itmail@yeah.net");
            //sQLAction.InsertData("db_Users", t, t1);
            // sQLAction.UpdateOrCreateData("db_Users", t, t1, "UID = 10001");
            //API.Print(SQLT_Operate.TSQL_Read<string>("db_Users", "UID = 10001", t)[0]);
            //API.SendMail("mail@menherachan");
            //API.Print(Analysis.GetReturnMessage($"Sendverification&3563640373@qq.com"));




            StartServer();

        }

        public static void StartServer ()
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
        public static void HandleClient (ClientInfo client)
        {
            NetworkStream stream = client.TcpClient.GetStream();

            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead;
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
}
/*
       　  　▃▆█▇▄▖
　 　 　 ▟◤▖　　　◥█▎
   　 ◢◤　 ▐　　　 　▐▉
　 ▗◤　　　▂　▗▖　　▕█▎
　◤　▗▅▖◥▄　▀◣　　█▊
▐　▕▎◥▖◣◤　　　　◢██
█◣　◥▅█▀　　　　▐██◤
▐█▙▂　　     　◢██◤
◥██◣　　　　◢▄◤
 　　▀██▅▇▀
*/



