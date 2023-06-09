using System.Data.SqlClient;

public static class SQLT_Operate
{
    private static readonly string connectionString = "Server=Zhangzijian\\SQLEXPRESS;Database=Users;User Id=sa;Password=Menherachan0822"; // 连接字符串


    private static readonly SqlConnectionPool pool = new SqlConnectionPool(connectionString);

    //增
    public static bool TSQL_ADD (string _DataBase_Name, string _Table_Name, string[] _List_Name, string[] _List_Value)
    {
        using (SqlConnection conn = pool.GetConnection())
        {
            //构造 insert 语句
            string sql = $"INSERT INTO {_DataBase_Name}.{_Table_Name} ({string.Join(", ", _List_Name)}) VALUES ({string.Join(", ", _List_Value)})";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }

    //删
    public static bool TSQL_Delete (string _DataBase_Name, string _Table_Name, string[] _List_Name, string[] _List_Value)
    {
        using (SqlConnection conn = pool.GetConnection())
        {
            //构造 delete 语句
            List<string> conditions = new List<string>();
            for (int i = 0; i < _List_Name.Length; i++)
            {
                conditions.Add($"{_List_Name[i]}='{_List_Value[i]}'");
            }

            string sql = $"DELETE FROM {_DataBase_Name}.{_Table_Name} WHERE {string.Join(" AND ", conditions)}";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }

    //改
    public static bool TSQL_Update (string _DataBase_Name, string _Table_Name, string _Condition, string[] _List_Name, string[] _List_Value)
    {
        using (SqlConnection conn = pool.GetConnection())
        {
            //构造 update 语句
            List<string> assignments = new List<string>();
            for (int i = 0; i < _List_Name.Length; i++)
            {
                assignments.Add($"{_List_Name[i]}='{_List_Value[i]}'");
            }

            string sql = $"UPDATE {_DataBase_Name}.{_Table_Name} SET {string.Join(", ", assignments)} WHERE {_Condition}";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
    }

    //查
    public static List<T> TSQL_Read<T> (string _DataBase_Name, string _Table_Name, string _Condition, string[] _List_Name)
    {
        using (SqlConnection conn = pool.GetConnection())
        {
            //构造 select 语句
            string selectColumns = string.Join(", ", _List_Name);
            string sql = $"SELECT {selectColumns} FROM {_DataBase_Name}.{_Table_Name} WHERE {_Condition}";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                List<T> results = new List<T>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T result = (T)reader[0];
                        results.Add(result);
                    }
                }

                return results;
            }
        }
    }
}
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
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        return new SqlConnection(_connectionString);
    }

    public void ReturnConnection (SqlConnection connection)
    {
        _connectionPool.Enqueue(connection);
    }
}
