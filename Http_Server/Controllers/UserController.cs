using Microsoft.AspNetCore.Mvc;
using SDK;

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

        [HttpGet]
        public IEnumerable<string> Get_HOiLAI ([FromQuery] string action)
        {
            string _res = Analysis.GetReturnMessage(action);

            yield return _res;
        }
    }
}