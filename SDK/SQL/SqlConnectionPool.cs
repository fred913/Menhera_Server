using System.Data.SqlClient;
namespace SDK.SQL.SqlConnectionPool
{
    public class SqlConnectionPool
    {
        private static string _connectionString = ConnectionString.GetconnectionString("Users");
        private static Queue<SqlConnection> _connectionPool = new Queue<SqlConnection>();

        public static SqlConnection GetConnection ()
        {
            if (_connectionPool.TryDequeue(out var connection))
            {
                connection.ConnectionString = _connectionString;
                return connection;
            }

            return new SqlConnection(_connectionString);
        }

        public static void ReturnConnection (SqlConnection connection)
        {
            try
            {
                connection.Close();
                _connectionPool.Enqueue(connection);
            }
            catch (Exception ex)
            {
                SDK.API.API.Print(ex.Message);

            }

        }
    }
}
