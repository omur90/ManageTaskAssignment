
using ManageTaskAssignment.SharedObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageTaskAssignment.Assignment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AssignmentController : CustomBaseController
    {
        [HttpGet] 
        [Authorize(Policy = "UserTokenPolicy")]
        public IActionResult TestUser()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult TestClient()
        {
            return Ok();
        }
    }
}
