using Microsoft.AspNetCore.Mvc;
using SDK;
using SDK.GameSDKS;

namespace Http_Server.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public IEnumerable<string> UserLogin (string username, string password)
        {
            Users user = new Users("Users");
            API.Print(username, " ", password);
            //目前仅支持邮箱地址登录，以后开放其他方式登录
            //其他方式登录，只需要更改下面的$"EmailAddress = '{username.Trim()}'"，使用方法API.CheckString()就可以判断出登录方式
            if (!user.IsPassword($"EmailAddress = '{username.Trim()}'", password.Trim()))
            {
                yield return "{status:false,msg:用户名或密码错误}";
            }
            else yield return "{status:true}";
        }
        [HttpGet]
        public IEnumerable<string> UserRegister (string username, string password)
        {
            Users user = new Users("Users");
            int num = user.SignUpNewUser(username.Trim(), password.Trim());
            switch (num)
            {
                case -1:
                    yield return "{status:false,msg:邮箱已经注册}";
                    break;
                case -2:
                    yield return "status:false,msg:注册失败,请联系管理员";
                    break;
                default:
                    yield return $"status:true,msg:注册成功,您的UID为,{num}";
                    break;
            }
        }
    }
}