
using ManageTaskAssignment.Assignment.Api.CQRS.Commands;
using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using ManageTaskAssignment.SharedObjects.Services;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ISharedIdentityService sharedIdentityService;

        public WorkOrderService(IMediator mediator, IHttpContextAccessor contextAccessor, ISharedIdentityService sharedIdentityService)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
            this.sharedIdentityService = sharedIdentityService;
        }

        public async Task<GenericResponse<NoContent>> CompleteWorkOrderAsync(CompleteWorkOrderDto workOrderDetail, CancellationToken cancellationToken)
        {
            try
            {
                return await mediator.Send(new CompleteWorkOrderCommand { DetailsOfTask = workOrderDetail.DetailsOfTask, UpdatedBy = workOrderDetail.UpdatedBy, WorkOrderId = workOrderDetail.WorkOrderId }, cancellationToken);
            }
            catch (Exception ex)
            {
                return GenericResponse<NoContent>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async Task<GenericResponse<NoContent>> CreateWorkOrderAsync(CreateWorkOrderDto workOrder, CancellationToken cancellationToken)
        {
            try
            {
                return await mediator.Send(new CreateWorkOrderCommand { CreatedBy = workOrder.CreatedBy, EmployeeId = workOrder.EmployeeId, TaskId = workOrder.TaskId }, cancellationToken);
            }
            catch (Exception ex)
            {
                return GenericResponse<NoContent>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async Task<GenericResponse<List<GetWorkOrderDto>>> GetWorkOrdersByEmployeeAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await mediator.Send(new GetWorkOrdersByEmployeeQuery { EmployeeId = sharedIdentityService.EmployeeId }, cancellationToken);
            }
            catch (Exception ex)
            {
                return GenericResponse<List<GetWorkOrderDto>>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async Task<GenericResponse<GetWorkOrderDto>> GetWorkOrderByTaskAsync(Guid taskId, CancellationToken cancellationToken)
        {
            try
            {
                return await mediator.Send(new GetWorkOrderByTaskQuery { EmployeeId = sharedIdentityService.EmployeeId, TaskId = taskId }, cancellationToken);
            }
            catch (Exception ex)
            {
                return GenericResponse<GetWorkOrderDto>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }
    }
}
