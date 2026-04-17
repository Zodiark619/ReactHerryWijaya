using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Utility.Project2Exercise;

namespace ReactApp1.Server.Controllers.Project2Exercise
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> GetSomething()
        {
            return "You are authorized user";
        }
        [HttpGet("{value:int}")]
        [Authorize(Roles =SD.Role_Admin)]
        public ActionResult<string> GetSomething(int value)
        {
            return "You are authorized user, with r ole admin";
        }
    }
}
