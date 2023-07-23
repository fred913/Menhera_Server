using Microsoft.AspNetCore.Mvc;
using SDk;
namespace Http_Server.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public IEnumerable<string> Post_HOiLAI ([FromForm] string action)
        {
            string _res = Analysis.GetReturnMessage(action);

            yield return _res;
        }
        [HttpPost]
        public IEnumerable<string> Post_Test ()
        {
            yield return $"status:true,res:Hello World！";
        }
    }
}