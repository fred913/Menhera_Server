using Microsoft.AspNetCore.Mvc;
using SDk;
namespace Http_Server.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public IEnumerable<string> Post_HOiLAI (string action)
        {
            string _res = Analysis.GetReturnMessage(action);

            yield return $"status:true,res:{_res}";
        }
        [HttpPost]
        public IEnumerable<string> Post_Test (string action)
        {
            yield return $"status:true,res:Hello World！";
        }
    }
}