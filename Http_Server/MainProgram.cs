using System.Net;

public class Example
{
    public static void Main (string[] args)
    {
        // 创建一个新的 HttpListener 对象
        HttpListener listener = new HttpListener();

        // 设置监听的 URL 前缀
        listener.Prefixes.Add("http://127.0.0.1:4050/");

        // 开始监听请求
        listener.Start();
        Console.WriteLine("Listening for requests on http://127.0.0.1:4050");

        // 处理请求
        while (true)
        {
            try
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                // 从请求正文中读取数据
                string postData = "";
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    postData = reader.ReadToEnd();
                }
                Console.WriteLine(postData);
                // 处理数据并返回响应
                string responseString = $"Received login info: {postData}";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            catch { }
        }
    }
}
