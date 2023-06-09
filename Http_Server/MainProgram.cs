using System.Net;

public class Example
{
    public static void Main (string[] args)
    {
        HttpListener listener = new HttpListener();

        listener.Prefixes.Add("http://127.0.0.1:4050/");

        listener.Start();
        Console.WriteLine("Listening for requests on http://127.0.0.1:4050");

        while (true)
        {
            try
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string postData = "";
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    postData = reader.ReadToEnd();
                }
                Console.WriteLine(postData);
                string responseString = 服务器.Analysis.GetReturnMessage(postData);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
            catch (Exception ex) { API.Print(ex.Message); }
        }
    }
}
