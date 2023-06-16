using System.Data.SqlClient;

namespace SDK.SQL
{
    public class SqlConnectionPool
    {
        private readonly string _connectionString;
        private readonly Queue<SqlConnection> _connectionPool;

        public SqlConnectionPool (string connectionString)
        {
            _connectionString = connectionString;
            _connectionPool = new Queue<SqlConnection>();
        }

        public SqlConnection GetConnection ()
        {
            if (_connectionPool.TryDequeue(out var connection))
            {
                return connection;
            }

            return new SqlConnection(_connectionString);
        }

        public void ReturnConnection (SqlConnection connection)
        {
            connection.Close();
            _connectionPool.Enqueue(connection);
        }
    }
}
