using Microsoft.AspNetCore.Mvc;
using 服务器.GameSDKS;
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
            if (!user.IsPassword($"EmailAddress = '{username.Trim()}'", password.Trim()))
            {
                yield return "{status:false,msg:用户名或密码错误}";
            }
            yield return "{status:true}";
        }
        [HttpGet]
        public IEnumerable<string> UserRegister (string username, string password)
        {
            Users user = new Users("Users");
            int num = user.SignUpNewUser(username, password);
            switch (num)
            {
                case -1:
                    yield return "{status:false,msg:邮箱已经注册}";
                    break;
                case -2:
                    yield return "status:false,msg:注册失败，请联系管理员";
                    break;
                default:
                    yield return "{status:true,msg:注册成功}";
                    break;
            }
        }
    }
}