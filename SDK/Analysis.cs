using 服务器.GameSDKS;
using 服务器.SQL;

namespace 服务器
{
    public static class Analysis
    {
        public const char endl = '\n';

        /*
        public static string GetReturnMessage (string message)
        {
            switch (message)
            {
                case "Ver":
                    return "0.1.0";
                case "News":

                    return "胡桃日记(Menherachan)最新版本来啦" + endl + "1.账号的加入。";
                default:
                    string[] analysis = message.Split('&'); var sQLAction = new SQLAction("Users"); var us = new Users("Users");
                    switch (analysis[0])
                    {
                        case "SignUp"://SignUp&email&password&name

                            return us.SignUpNewUser(analysis[1], API.GetMD5(analysis[2]), analysis[3]).ToString();

                        case "Login"://Login&email&password

                            string returnres = sQLAction.SelectData("db_Users", API.GetArray<string>("UID", "PassWord"), $"EmailAddress = {analysis[1]}");
                            if (returnres.Split("&")[1] == API.GetMD5(analysis[2]))
                            {
                                return returnres.Split("&")[0];
                            }
                            else
                            {
                                return "账号或密码错误";
                            }

                        case "GetInfo"://GetInfo&condition&password&info

                            //condition UID = 10001
                            if ()




                                return sQLAction.SelectData("db_Users", API.GetArray<string>(analysis[3]), analysis[1]);

                        default:
                            return "Error";
                    }


            }

        }
        */
        public static string GetReturnMessage (string message)
        {
            //改为集合的方法
            var actions = new Dictionary<string, Func<string, string>>() {
                { "Ver", GetVersion },
                { "News", GetNews },
                { "SignUp", SignUp },
                { "Login", Login },
                {"GetInfo", Getinfo},
                {"UpdateInfo",UpdateInfo},
                { "SendMail",SendMail}
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
        /// SendMail & Email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string SendMail (string message)
        {
            string[] analysis = message.Split('&');
            if (message != "")
            {
                return API.SendMail("zhangzijian@menherachan.cn", "Menherachan0822", analysis[1], "您正在参与验证码服务", "您的验证码为:" + API.GetRandomInAB(100000, 999999)).ToString();


            }
            return "False";
            // throw new NotImplementedException();
        }

        private static string GetVersion (string message)
        {
            return "0.1.0";
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
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string Getinfo (string message)
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
                return "账号或密码错误";

            }
            else
            {
                try
                {

                    return SQLT_Operate.TSQL_Update(analysis[5], analysis[1], API.GetArray(analysis[3]), API.GetArray(analysis[4])).ToString();
                    //return user.UpdateUserInfo("db_Menherachan", analysis[1], analysis[3], analysis[4]).ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }

    }
}