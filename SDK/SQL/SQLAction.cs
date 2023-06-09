using System.Data.SqlClient;

namespace 服务器.SQL
{
    public class SQLAction
    {
        private readonly string connectionString;
        /// <summary>
        /// 实例化调用
        /// var sql = new SQLAction("localhost", "TestDB", "sa", "password");
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public SQLAction (string database)
        {
            connectionString = ConnectionString.GetconnectionString(database);
        }

        /// <summary>
        /// 使用范例：
        /// sql.InsertData("Employees", new[] { "Name", "Age", "Department" }, new object[] { "'John Smith'", 30, "'Sales'" });
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        public bool InsertData (string tableName, string[] columns, object[] values)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand($"INSERT INTO {tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", values)})", connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 使用范例：
        /// sql.UpdateData("Employees", new[] { "Age", "Department" }, new object[] { 31, "'Marketing'" }, "Name='John Smith'");
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        /// <param name="condition"></param>
        public void UpdateOrCreateData (string tableName, string[] columns, object[] values, string condition)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 检查是否存在符合条件的行



                var checkCommand = new SqlCommand($"SELECT * FROM {tableName} WHERE {condition}", connection);
                if (SelectData(tableName, columns, condition) == "None")
                {
                    InsertData(tableName, columns, values);
                }
                else
                {
                    // 更新符合条件的行

                    string setClause = string.Join(",", columns.Zip(values, (col, val) => $"{col}=@{col}"));
                    var updateCommand = new SqlCommand($"UPDATE {tableName} SET {setClause} WHERE {condition}", connection);
                    for (int i = 0; i < columns.Length; i++)
                    {
                        updateCommand.Parameters.AddWithValue($"@{columns[i]}", values[i]);
                    }
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    // API.Print($"{rowsAffected} row(s) updated.");
                }




            }

        }
        /// <summary>
        /// 使用范例：
        /// sql.DeleteData("Employees", "Department='Marketing'");
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        public void DeleteData (string tableName, string condition)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand($"DELETE FROM {tableName} WHERE {condition}", connection);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) deleted.");
            }
        }

        /// <summary>
        /// 使用范例：
        /// 查询指定列和条件的数据：sql.SelectData("Employees", new[] { "Name", "Age" }, "Department='Sales'");
        /// 查询所有行的数据：sql.SelectData("Employees");
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        public string SelectData (string tableName, string[] columns, string condition = "")//zhangzijian @itmail@yeah.net
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectClause = (columns?.Length > 0) ? string.Join(",", columns) : "*";
                var command = new SqlCommand($"SELECT {selectClause} FROM {tableName} {(string.IsNullOrEmpty(condition) ? "" : $"WHERE {condition}")}", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<string> values = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            {
                                values.Add($"{reader.GetValue(i)}");
                            }
                            return string.Join("&", values);
                        }
                    }
                }
            }
            return "None";
        }
        //By 一水久钟
    }
}
