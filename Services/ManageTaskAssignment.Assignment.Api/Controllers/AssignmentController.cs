
using ManageTaskAssignment.Assignment.Api.Dto;
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
        [Authorize(Policy = TokenConstants.UserTokenPolicy)]
        public async Task<IActionResult> GetAllWorkOrder(CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.GetAllWorkOrderAsync(cancellationToken));
        }

        [HttpGet]
        [Authorize(Policy = TokenConstants.UserTokenPolicy)]
        public async Task<IActionResult> GetWorkOrdersByEmployee(CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.GetWorkOrdersByEmployeeAsync(cancellationToken));
        }

        [HttpGet]
        [Authorize(Policy = TokenConstants.ClientTokenPolicy)]
        public async Task<IActionResult> GetWorkOrderByTask(GetWorkOrderByTaskDto getWorkOrderByTask, CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.GetWorkOrderByTaskAsync(getWorkOrderByTask.TaskId, cancellationToken));
        }

        [HttpPost]
        [Authorize(Policy = TokenConstants.UserTokenPolicy)]
        public async Task<IActionResult> CompleteWorkOrder(CompleteWorkOrderDto completeWorkOrder, CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.CompleteWorkOrderAsync(completeWorkOrder, cancellationToken));
        }

        [HttpPost]
        [Authorize(Policy = TokenConstants.UserTokenPolicy)]
        public async Task<IActionResult> CreateWorkOrder(CreateWorkOrderDto createWorkOrder, CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.CreateWorkOrderAsync(createWorkOrder, cancellationToken));
        }

        [HttpPost]
        [Authorize(Policy = TokenConstants.UserTokenPolicy)]
        public async Task<IActionResult> CancelWorkOrder(CancelWorkOrderDto cancelWorkOrder, CancellationToken cancellationToken)
        {
            return CreateActionResult(await workOrderService.CancelWorkOrderAsync(cancelWorkOrder, cancellationToken));
        }
    }
}
