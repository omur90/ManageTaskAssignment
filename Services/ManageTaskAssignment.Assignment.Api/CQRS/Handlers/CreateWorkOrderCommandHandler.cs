using ManageTaskAssignment.Assignment.Api.CQRS.Commands;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class CreateWorkOrderCommandHandler : IRequestHandler<CreateWorkOrderCommand, GenericResponse<NoContent>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;
        private IHttpContextAccessor contextAccessor;

        public CreateWorkOrderCommandHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<NoContent>> Handle(CreateWorkOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.EmployeeId)} can not be empty value !");
            }

            if (string.IsNullOrWhiteSpace(request.TaskId))
            {
                throw new CustomBusinessException($"{nameof(request.TaskId)} can not be null or empty value !");
            }

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                throw new CustomBusinessException($"{nameof(request.CreatedBy)} can not be null or empty value !");
            }

            await workOrderDbContext.WorkOrders.AddAsync(new Entities.WorkOrder
            {
                CreatedBy = request.CreatedBy, 
                TaskId = request.TaskId,
                CreatedDate = DateTime.Now,
                IsOpen = true,
                EmployeeId = request.EmployeeId,
                StatusId = (int)WorkOrderStatusType.WaitingToAssign
            }, cancellationToken);

            await workOrderDbContext.SaveChangesAsync(cancellationToken);

            return GenericResponse<NoContent>.Sucess(contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
