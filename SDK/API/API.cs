﻿using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SDK
{
    public static class API
    {

        /// <summary>
        /// 在[minA，maxB]中产生一个随机数，并返回它
        /// </summary>
        /// <param name="minA"></param>
        /// <param name="maxB"></param>
        /// <returns></returns>
        public static int GetRandomInAB (int minA, int maxB)
        {
            if (maxB > minA)
            {
                byte[] bytes = new byte[4];
                RandomNumberGenerator.Fill(bytes);
                int seed = BitConverter.ToInt32(bytes, 0);

                string strTick = Convert.ToString(DateTime.Now.Ticks);
                if (strTick.Length > 8)
                    strTick = strTick.Substring(strTick.Length - 8, 8);
                seed = seed + Convert.ToInt32(strTick);
                Random random = new Random(seed);
                return random.Next(minA, maxB);
            }
            else
            {
                return -1; // 返回 -1 表示参数无效
            }
        }

        /// <summary>
        /// 接管Console.WriteLine(...)
        /// </summary>
        /// <param name="values"></param>
        public static void Print (params object[] values)
        {
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string message = string.Join(" ", values);
            Console.WriteLine($"[{timestamp}] {message}");
        }
        public static string GetMD5 (string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public static bool SendMail (string email, string password, string toEmail, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.exmail.qq.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(email, password);
                MailMessage mailMessage = new MailMessage(email, toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                client.Send(mailMessage);

                return true; // 发送成功，返回 true
            }
            catch (Exception ex)
            {
                Console.WriteLine($"邮件发送失败: {ex.Message}");
                return false; // 发送失败，返回 false
            }
        }
        public static bool Getverification (string toemail)
        {
            try
            {
                string verification = GetRandomInAB(100000, 999999).ToString();
                SendMail("zhangzijian@menherachan.cn", "Cat.jiuzhong0910", toemail, "验证码", $"您的验证码为:{verification}");

                //  SQLAction sQLAction = new SQLAction("Zhangzijian\\SQLEXPRESS", "Menherachan_Pwms", "sa", "Menherachan0822");
                //sQLAction.UpdateOrCreateData("Menherachan_CAPTCHA", new[] { "CAPTCHA" }, new[] { verification }, "Email = '" + toemail + "'");
                return true;
            }
            catch (Exception ex)
            {
                API.Print(ex.Message);
                return false;

            }
        }
        public static T[] GetArray<T> (params T[] values)
        {
            T[] array = new T[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                array[i] = values[i];
            }
            return array;
        }
        public static int CheckString (string input)
        {
            // 检查字符串是否为邮箱地址
            if (IsValidEmail(input))
                return 1;

            // 检查字符串是否为纯数字
            if (IsNumeric(input))
                return 2;

            // 如果既不是邮箱地址也不是纯数字，则返回-1
            return -1;
        }

        // 检查字符串是否为有效的邮箱地址
        public static bool IsValidEmail (string email)
        {
            // 使用合适的正则表达式来验证邮箱地址是否有效
            // 这里只是一个简单的示例，请使用更精确和完善的正则表达式
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        // 检查字符串是否为纯数字
        public static bool IsNumeric (string input)
        {
            // 使用int.TryParse()方法将字符串尝试转换为整数
            int number;
            return int.TryParse(input, out number);
        }
    }
}

