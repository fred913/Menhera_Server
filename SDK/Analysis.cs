﻿using 服务器.GameSDKS;
using 服务器.SQL;

namespace 服务器
{
    public static class Analysis
    {
        const char endl = '\n';

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

        private static string GetVersion (string message)
        {
            return "0.1.0";
        }

        private static string GetNews (string message)
        {
            return "胡桃日记(Menherachan)最新版本来啦" + endl + "1.账号的加入。";
        }
        /// <summary>
        /// SignUp&email&password&name
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string SignUp (string message)
        {
            try
            {
                string[] analysis = message.Split('&'); var us = new Users("Users");
                return us.SignUpNewUser(analysis[1], analysis[2], analysis[3]).ToString();
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
    }
}