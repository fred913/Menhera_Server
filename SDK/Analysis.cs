using SDK;
using SDK.GameSDKS;
using SQL;
using System.Text.Encodings.Web;

using System.Text.Json;
namespace SDk
{
    public static class Analysis
    {

        public const char endl = '\n';


        public static string GetReturnMessage (string message)
        {
            //改为集合的方法
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
                { "GetUserInfo",GetUserInfo}

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
                string[] analysis = message.Split('&');
                List<string> userinfo = SQLT_Operate.TSQL_Read("db_Users", analysis[1], API.GetArray("UserName", "EmailAddress", "QQ", "isEnable", "Gender_Sex"));
                API.Print(userinfo[0]);
                var userInfo = new UserInfo
                {
                    UserName = userinfo[0],
                    EmailAddress = userinfo[1],
                    QQ = userinfo[2],
                    isEnable = userinfo[3]
                };

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(userInfo, options);
                API.Print(json);
                return json;
            }
            catch
            {
                return (-1).ToString();
            }
        }

    }
}