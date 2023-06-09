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

        }
    }
}
