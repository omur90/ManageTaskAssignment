
using ManageTaskAssignment.Assignment.Api.CQRS.Commands;
using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        public WorkOrderService(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
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

        public async Task<GenericResponse<List<GetWorkOrderByEmployeeDto>>> GetWorkOrdersByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            try
            {
                return await mediator.Send(new GetWorkOrderByEmployeeQuery { EmployeeId = employeeId }, cancellationToken);
            }
            catch (Exception ex)
            {
                return GenericResponse<List<GetWorkOrderByEmployeeDto>>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }
        public Task<GenericResponse<GetWorkOrderByEmployeeDto>> GetWorkOrderItemByEmployeeId(Guid employeeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
