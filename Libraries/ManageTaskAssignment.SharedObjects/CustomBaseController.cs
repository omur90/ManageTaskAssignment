using Microsoft.AspNetCore.Mvc;

namespace ManageTaskAssignment.SharedObjects
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(GenericResponse<T> data)
        {
            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }
    }
}
