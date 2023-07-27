using SDK.SQL.SqlConnectionPool;
using System.Data.SqlClient;
namespace SQL
{
    public static class SQLT_Operate
    {
        //增

        /// <summary>
        /// 顾名思义，这是一个插入的方法。
        /// 但是警告：不要使用该方法注册新用户！
        /// 因为该方法在插入的时候不会检测是否已经存在插入数据，因此会出现重复用户！
        /// </summary>
        /// <param name="_Table_Name"></param>
        /// <param name="_List_Name"></param>
        /// <param name="_List_Value"></param>
        /// <returns></returns>
        public static bool TSQL_ADD (string _Table_Name, string[] _List_Name, string[] _List_Value)
        {
            using (SqlConnection conn = SqlConnectionPool.GetConnection())
            {
                //构造 insert 语句
                conn.Open();
                string sql = $"INSERT INTO {_Table_Name} ({string.Join(", ", _List_Name)}) VALUES ({string.Join(", ", _List_Value)})";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int result = cmd.ExecuteNonQuery();
                    SqlConnectionPool.ReturnConnection(conn);
                    return result > 0;
                }
            }
        }

        //删
        public static bool TSQL_Delete (string _Table_Name, string[] _List_Name, string[] _List_Value)
        {
            using (SqlConnection conn = SqlConnectionPool.GetConnection())
            {
                conn.Open();
                //构造 delete 语句
                List<string> conditions = new List<string>();
                for (int i = 0; i < _List_Name.Length; i++)
                {
                    conditions.Add($"{_List_Name[i]}='{_List_Value[i]}'");
                }

                string sql = $"DELETE FROM {_Table_Name} WHERE {string.Join(" AND ", conditions)}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int result = cmd.ExecuteNonQuery();
                    SqlConnectionPool.ReturnConnection(conn);
                    return result > 0;
                }
            }
        }

        //改
        public static bool TSQL_Update (string _Table_Name, string _Condition, string[] _List_Name, string[] _List_Value)
        {
            using (SqlConnection conn = SqlConnectionPool.GetConnection())
            {
                conn.Open();
                //构造 update 语句
                List<string> assignments = new List<string>();
                for (int i = 0; i < _List_Name.Length; i++)
                {
                    assignments.Add($"{_List_Name[i]}='{_List_Value[i]}'");
                }

                string sql = $"UPDATE {_Table_Name} SET {string.Join(", ", assignments)} WHERE {_Condition}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    int result = cmd.ExecuteNonQuery();
                    SqlConnectionPool.ReturnConnection(conn);
                    return result > 0;
                }
            }
        }


        /// <summary>
        /// 这个有问题，不建议使用。
        /// 如果一定要使用，请确保返回的值中的数据类型全部一致，否则会出错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_Table_Name"></param>
        /// <param name="_Condition"></param>
        /// <param name="_List_Name"></param>
        /// <returns></returns>
        public static List<T> TSQL_Read<T> (string _Table_Name, string _Condition, string[] _List_Name)
        {
            using (SqlConnection conn = SqlConnectionPool.GetConnection())
            {
                conn.Open();
                //构造 select 语句
                string selectColumns = string.Join(", ", _List_Name);
                string sql = $"SELECT {selectColumns} FROM {_Table_Name} WHERE {_Condition}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<T> results = new List<T>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < _List_Name.Length; i++)
                            {
                                //API.Print(reader[i]);
                                T result = (T)reader[i];
                                results.Add(result);
                            }
                        }
                    }
                    SqlConnectionPool.ReturnConnection(conn);
                    return results;
                }
            }
        }

        /// <summary>
        /// TSQL_Read<T>的重载版本。
        /// </summary>
        /// <param name="_Table_Name"></param>
        /// <param name="_Condition"></param>
        /// <param name="_List_Name"></param>
        /// <returns></returns>
        public static List<string> TSQL_Read (string _Table_Name, string _Condition, string[] _List_Name)
        {
            using (SqlConnection conn = SqlConnectionPool.GetConnection())
            {
                conn.Open();
                //构造 select 语句
                string selectColumns = string.Join(", ", _List_Name);
                string sql = $"SELECT {selectColumns} FROM {_Table_Name} WHERE {_Condition}";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    List<string> results = new List<string>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < _List_Name.Length; i++)
                            {
                                //API.Print(reader[i]);
                                string result = reader[i].ToString();
                                result = System.Text.RegularExpressions.Regex.Unescape(result); // 还原转义序列
                                results.Add(result);
                            }
                        }
                    }
                    SqlConnectionPool.ReturnConnection(conn);
                    return results;
                }
            }
        }



    }


}

