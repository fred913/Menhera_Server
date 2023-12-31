﻿using SDK.GameSDKS;
using SDK.HOILAI_Community;
using SQL;
using System.Text.Encodings.Web;

using System.Text.Json;
namespace SDK
{
    public static class Analysis
    {

        public const char endl = '\n';


        public static string GetReturnMessage (string message)
        {
            //分割字符串判断api类型
            var actions = new Dictionary<string, Func<string, string>>()
            {
                { "Ver", GetVersion },
                { "News", GetNews },
                { "SignUp", SignUp },
                { "Login", Login },
                {"GetInfo", Getinfo},
                {"UpdateInfo",UpdateInfo},
                { "Sendverification",Sendverification},
                { "ResettingPassword",ResettingPassword},
                { "GetUserInfo",GetUserInfo},
                { "UpdateUserInfo",UpdateUserInfo},
                { "AddNewArticle",AddNewArticle},
                { "GetOfficialArticles",GetOfficialArticles}

        };
            var parts = message.Split('&');
            var actionName = parts[0];

            if (actions.ContainsKey(actionName))
            {
                var action = actions[actionName];
                return action.Invoke(message);
            }
            else
            {
                return "Error";
            }
        }

        /// <summary>
        /// Sendverification & Email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string Sendverification (string message)
        {
            string[] analysis = message.Split('&');
            //analysis[1]是要发送到的邮箱
            if (message != "")
            {
                try
                {
                    //return API.SendMail("zhangzijian@menherachan.cn", "Menherachan0822", analysis[1], "您正在参与验证码服务", "您的验证码为:" + API.GetRandomInAB(100000, 999999)).ToString();
                    Thread t = new Thread(() => { API.Getverification(analysis[1]); });
                    t.Start();
                    return true.ToString();
                }
                catch (Exception ex)
                {
                    API.Print(ex.Message);
                    return false.ToString();
                }
            }
            return false.ToString();
            // throw new NotImplementedException();
        }
        private static string GetVersion (string message)
        {
            var ini = new SDK.INIFile("config.ini");
            return ini.Read("GameInfo", "Ver");
        }

        private static string GetNews (string message)
        {
            return "胡桃日记(Menherachan)最新版本来啦" + endl + "1.账号的加入。";
        }
        /// <summary>
        /// SignUp&email&password
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string SignUp (string message)
        {
            try
            {
                string[] analysis = message.Split('&'); var us = new Users("Users");
                return us.SignUpNewUser(analysis[1], analysis[2]).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// Login&email&password
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string Login (string message)
        {
            try
            {

                string[] analysis = message.Split('&'); var sQLAction = new SQLAction("Users");
                string returnres = sQLAction.SelectData("db_Users", API.GetArray("UID"), $"EmailAddress = '{analysis[1]}' AND PassWord = '{analysis[2]}'");
                if (returnres != "None")
                {
                    return returnres.Split("&")[0];
                }
                else { return "没有找到该用户"; }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// Getinfo&condition&password&tablename&getname
        /// -1:账号或密码错误
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string Getinfo (string message)
        {
            try
            {
                string[] analysis = message.Split('&'); var sQLAction = new SQLAction("Users"); var user = new Users("Users"); string[] t = new string[] { analysis[4] };
                if (!user.IsPassword(analysis[1], analysis[2]))
                {
                    return "账号或密码错误";
                }
                else
                {
                    return sQLAction.SelectData(analysis[3], t, analysis[1]);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        ///  UpdateInfo & condition(UID) & password & Listname & value & tablename
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string UpdateInfo (string message)
        {
            string[] analysis = message.Split('&'); var user = new Users("Users");
            if (!user.IsPassword(analysis[1], analysis[2]))
            {
                return (-1).ToString();
            }
            else
            {
                try
                {
                    if (SQLT_Operate.TSQL_Update(analysis[5], analysis[1], API.GetArray(analysis[3]), API.GetArray(analysis[4])))
                    {

                        return 1.ToString();
                    }
                    else return 0.ToString();
                    //return user.UpdateUserInfo("db_Menherachan", analysis[1], analysis[3], analysis[4]).ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// 重置账户密码
        ///ResettingPassword&条件&原来的密码&新密码
        ///-1:账号或密码错误
        ///1:修改成功
        ///0:修改失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string ResettingPassword (string message)
        {
            try
            {
                string[] analysis = message.Split('&');
                var user = new Users("Users");
                if (user.IsPassword(analysis[1], analysis[2]))
                {
                    if (user.UpdateUserInfo("db_Users", analysis[1], "PassWord", analysis[3]))
                    {
                        return 1.ToString();
                    }
                    else return 0.ToString();

                }
                else return (-1).ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary>
        /// 获取用户信息
        /// GetUserInfo & 条件 & 密码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string GetUserInfo (string message)
        {
            try
            {
                var user = new Users("Users");
                string[] analysis = message.Split('&');
                if (user.IsPassword(analysis[1], analysis[2]))
                {
                    List<string> userinfo = SQLT_Operate.TSQL_Read("db_Users", analysis[1], API.GetArray("UserName", "EmailAddress", "QQ", "isEnable", "Gender_Sex"));
                    API.Print(userinfo[0]);
                    var userInfo = new UserInfo
                    {
                        UserName = userinfo[0],
                        EmailAddress = userinfo[1],
                        QQ = userinfo[2],
                        isEnable = userinfo[3],
                        Gender_Sex = userinfo[4]
                    };

                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    };
                    string json = JsonSerializer.Serialize(userInfo, options);
                    //API.Print(json);
                    return json;
                }
                else
                {
                    return (-1).ToString();//密码错误
                }

            }
            catch
            {
                return (-2).ToString();//系统出错
            }
        }
        /// <summary>
        /// 
        ///  UpdateInfo & condition(UID) & password & tablename & Listname & value 
        ///  其中，& Listname & value 使用,拼接|拼接
        ///  -1:密码错误
        ///  0:系统错误
        ///  1:操作成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string UpdateUserInfo (string message)
        {
            string[] analysis = message.Split('&'); var user = new Users("Users");
            if (!user.IsPassword(analysis[1], analysis[2]))
            {
                return (-1).ToString();
            }
            else
            {
                try
                {// //UpdateInfo&UID = 10001&f36bb8bcda27e0e0ceb6e4bc3a64a506&db_Users&UserName&一水久钟
                    if (SQLT_Operate.TSQL_Update(analysis[3], analysis[1], analysis[4].Split('|'), analysis[5].Split('|')))
                    {

                        return 1.ToString();
                    }
                    else return 0.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        /// <summary>
        /// 新建一个文章
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string AddNewArticle (string message)
        {
            string[] analysis = message.Split('&');
            var _time = new DateTime();
            DateTime.TryParse(analysis[2], out _time);
            try
            {
                return API_Article.AddArticle_tb_OfficiaNews(analysis[0], Convert.ToInt32(analysis[1]), _time, analysis[3], analysis[4]);
            }
            catch (Exception ex)
            {
                API.Print(ex.Message);
                return ex.Message;
            }

        }

        /// <summary>
        /// 返回json格式的官方动态信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string GetOfficialArticles (string message)
        {
            try
            {
                return API_Article.GetOfficialArticles();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        /// <summary>
        /// 官方的动态点赞
        /// OfficialArticleLikes & 要点赞的文章id 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string OfficialArticleLikes (string message)
        {
            try
            {
                return "{state:true;title:权限不足}";
                return 1.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}