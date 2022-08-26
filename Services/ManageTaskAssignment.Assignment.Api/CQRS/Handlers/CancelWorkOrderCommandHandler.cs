using AutoMapper;
using ManageTaskAssignment.Assignment.Api.CQRS.Commands;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class CancelWorkOrderCommandHandler : IRequestHandler<CancelWorkOrderCommand, GenericResponse<NoContent>>
    {

        private readonly WorkOrderDbContext workOrderDbContext;

        private readonly IHttpContextAccessor contextAccessor;

        public CancelWorkOrderCommandHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<NoContent>> Handle(CancelWorkOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.WorkOrderId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.WorkOrderId)} can not be empty value !");
            }

            var workOrder = await workOrderDbContext.WorkOrders.Where(x => x.Id == request.WorkOrderId).FirstOrDefaultAsync();

            if (workOrder == null)
            {
                throw new CustomBusinessException($"{nameof(workOrder)} can not find !");
            }

            if (!workOrder.IsOpen)
            {
                throw new CustomBusinessException("Can not update after the job is done !");
            }

            if (workOrder.EmployeeId != request.EmployeeId)
            {
                throw new CustomBusinessException("Can not cancel cause your token does not match !");
            }

            workOrder.UpdatedDate = DateTime.UtcNow;
            workOrder.UpdatedBy = request.UpdatedBy;
            workOrder.IsOpen = false;
            workOrder.StatusId = (int)WorkOrderStatusType.Cancelled;

            workOrderDbContext.Update(workOrder);
            await workOrderDbContext.SaveChangesAsync();

            return GenericResponse<NoContent>.Sucess(contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
