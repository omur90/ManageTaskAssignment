
using ManageTaskAssignment.Assignment.Api.Services;
using ManageTaskAssignment.SharedObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageTaskAssignment.Assignment.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AssignmentController : CustomBaseController
    {
        private readonly IWorkOrderService workOrderService;

        public AssignmentController(IWorkOrderService workOrderService)
        {
            this.workOrderService = workOrderService;
        }

        [HttpGet] 
        [Authorize(Policy = "UserToken")]
        public async Task<IActionResult> GetWorkOrderByEmployee(CancellationToken cancellationToken)
        {
            //TODO : Token dan sub claim i alarak employeeId bulunmalı.
            var id = Guid.NewGuid();
            return CreateActionResult(await workOrderService.GetWorkOrdersByEmployeeAsync(id, cancellationToken));
        }

        [HttpGet]
        [Authorize(Policy = "ClientToken")]
        public IActionResult TestClient()
        {
            return Ok();
        }
    }
}
