public class ConnectionString
{
    public static string GetconnectionString (string database)
    {
        return $"Server=Zhangzijian\\SQLEXPRESS;Database={database} ;User Id=sa;Password=Menherachan0822";
    }
}
