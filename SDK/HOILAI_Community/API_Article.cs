using System.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace SDK.HOILAI_Community
{
    public static class API_Article
    {
        /// <summary>
        /// 添加一个文章
        /// </summary>
        /// <param name="authorName">作者名</param>
        /// <param name="authorUID">作者UID</param>
        /// <param name="publishTime">发布日期</param>
        /// <param name="contentAddress">文章地址</param>
        public static string AddArticle_tb_OfficiaNews (string authorName, int authorUID, DateTime publishTime, string contentAddress, string articleTitle)
        {
            string insertQuery = "INSERT INTO tb_OfficiaNews (AuthorName, AuthorUID, PublishTime, ContentAddress, ArticleTitle) " +
                                 "VALUES (@AuthorName, @AuthorUID, @PublishTime, @ContentAddress, @ArticleTitle)";
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetconnectionString("db_Article")))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@AuthorName", authorName);
                command.Parameters.AddWithValue("@AuthorUID", authorUID);
                command.Parameters.AddWithValue("@PublishTime", publishTime);
                command.Parameters.AddWithValue("@ContentAddress", contentAddress);
                command.Parameters.AddWithValue("@ArticleTitle", articleTitle);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true.ToString();
                }
                catch (Exception ex)
                {
                    API.Print(ex.Message);
                    return false.ToString();
                }
            }
        }
        public static string GetOfficialArticles ()
        {
            string connectionString = ConnectionString.GetconnectionString("db_Article");
            string sqlQuery = "SELECT\r\n  [ArticleID],\r\n  [ArticleTitle],\r\n  [AuthorName],\r\n  [AuthorUID],\r\n  [PublishTime],\r\n  [Likes],\r\n  [ContentAddress]\r\nFROM [db_Article].[dbo].[tb_OfficiaNews]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                while (reader.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    rows.Add(row);
                }

                connection.Close();
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                string jsonResult = JsonSerializer.Serialize(rows, options);
                return jsonResult;
            }
        }
    }
}
