
using SDK.SQL.SqlConnectionPool;
using System.Data.SqlClient;

namespace SDKS.GameSDKS
{
    public class Users
    {
        private readonly string connectionString;
        public Users (string database)
        {
            connectionString = ConnectionString.GetconnectionString(database);
        }
        /// <summary>
        /// 注册新用户。
        /// </summary>
        /// <param name="emailAddress">用户的邮箱地址。</param>
        /// <param name="password">用户密码。</param>
        /// <returns>返回注册用户的实际 ID 值，如果注册失败则返回 -1。</returns>
        public int SignUpNewUser (string emailAddress, string password)
        {
            using (var connection = SqlConnectionPool.GetConnection())
            {
                connection.Open();
                var checkCommand = new SqlCommand($"SELECT COUNT(*) FROM db_Users WHERE EmailAddress = '{emailAddress}'", connection);
                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {

                    //邮箱是否已经注册过了
                    SqlConnectionPool.ReturnConnection(connection);
                    return -1;
                }

                //name
                var insertCommand = new SqlCommand($"INSERT INTO db_Users (EmailAddress, PassWord) VALUES ('{emailAddress}', '{password}'); SELECT SCOPE_IDENTITY();", connection);
                int newID = Convert.ToInt32(insertCommand.ExecuteScalar());
                if (newID == 0)
                {
                    //插入失败
                    SqlConnectionPool.ReturnConnection(connection);
                    return -2;
                }
                else
                {
                    //返回新创建的用户ID
                    SqlConnectionPool.ReturnConnection(connection);
                    return newID;
                }
            }
        }
        // <summary>
        /// 更新用户信息。
        /// </summary>
        /// <param name="id">需要更新的用户 ID。</param>
        /// <param name="columnName">需要更新的列名。</param>
        /// <param name="columnValue">新的列值。</param>
        /// <returns>返回一个标志值，表示更新操作是否成功。</returns>
        public bool UpdateUserInfo (string tablename, string id, string columnName, string columnValue)
        {
            using (var connection = SqlConnectionPool.GetConnection())
            {
                connection.Open();

                // 检查该 ID 是否存在
                var checkCommand = new SqlCommand($"SELECT COUNT(*) FROM {tablename} WHERE UID={id}", connection);
                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    //API.Print("User does not exist.");
                    SqlConnectionPool.ReturnConnection(connection);
                    return false;
                }

                // 更新用户信息

                var updateCommand = new SqlCommand($"UPDATE {tablename} SET {columnName}=@value WHERE UID={id}", connection);
                updateCommand.Parameters.AddWithValue("@value", columnValue);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    //API.Print("Failed to update user information.");
                    SqlConnectionPool.ReturnConnection(connection);
                    return false;
                }
                else
                {
                    //API.Print("User information updated successfully.");
                    SqlConnectionPool.ReturnConnection(connection);
                    return true;
                }
            }
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsPassword (string condition, string password)
        {
            using (var connection = SqlConnectionPool.GetConnection())
            {
                connection.Open();

                // 查询指定邮箱的用户是否存在
                var checkCommand = new SqlCommand($"SELECT COUNT(*) FROM db_Users WHERE {condition}", connection);
                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    SqlConnectionPool.ReturnConnection(connection);
                    return false;
                }

                // 检查密码是否正确
                var checkPasswordCommand = new SqlCommand($"SELECT PassWord FROM db_Users WHERE {condition}", connection);
                string dbPassword = (string)checkPasswordCommand.ExecuteScalar();
                if (dbPassword != password)
                {
                    SqlConnectionPool.ReturnConnection(connection);
                    return false;
                }

                // 密码正确
                SqlConnectionPool.ReturnConnection(connection);
                return true;
            }
        }
    }
}