using ManageTaskAssignment.Assignment.Api.CQRS.Commands;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class CompleteWorkOrderCommandHandler : IRequestHandler<CompleteWorkOrderCommand, GenericResponse<NoContent>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;
        private IHttpContextAccessor contextAccessor;

        public CompleteWorkOrderCommandHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<NoContent>> Handle(CompleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.WorkOrderId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.WorkOrderId)} can not be empty value !");
            }

            if (string.IsNullOrWhiteSpace(request.DetailsOfTask))
            {
                throw new CustomBusinessException($"{nameof(request.DetailsOfTask)} can not be null or empty value !");
            }

            if (string.IsNullOrWhiteSpace(request.UpdatedBy))
            {
                throw new CustomBusinessException($"{nameof(request.UpdatedBy)} can not be null or empty value !");
            }

            var workOrderItem = await workOrderDbContext.WorkOrders.Where(x => x.Id == request.WorkOrderId).FirstOrDefaultAsync(cancellationToken);

            if (workOrderItem == null)
            {
                throw new CustomBusinessException($"{nameof(workOrderItem)} can not find !");
            }

            workOrderItem.IsOpen = false;
            workOrderItem.UpdatedBy = request.UpdatedBy;
            workOrderItem.UpdatedDate = DateTime.Now;
            workOrderItem.StatusId = (int)WorkOrderStatusType.Completed;

            workOrderDbContext.WorkOrders.Update(workOrderItem);

            await workOrderDbContext.WorkOrderDetails.AddAsync(new Entities.WorkOrderDetail
            {
                DetailsOfTask = request.DetailsOfTask,
                WorkOrderId = request.WorkOrderId,
            }, cancellationToken);

            await workOrderDbContext.SaveChangesAsync(cancellationToken);

            return GenericResponse<NoContent>.Sucess(contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
