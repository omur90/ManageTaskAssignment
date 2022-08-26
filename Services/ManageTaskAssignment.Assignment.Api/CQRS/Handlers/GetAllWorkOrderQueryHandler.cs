using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class GetAllWorkOrderQueryHandler : IRequestHandler<GetAllWorkOrderQuery, GenericResponse<List<GetAllWorkOrderDto>>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;

        private readonly IHttpContextAccessor contextAccessor;

        public GetAllWorkOrderQueryHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<List<GetAllWorkOrderDto>>> Handle(GetAllWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var responseItem = new List<GetAllWorkOrderDto>();

            var workOrders = await workOrderDbContext.WorkOrders.Include(x => x.WorkOrderDetail).ToListAsync(cancellationToken);

            if (workOrders.Any())
            {
                workOrders.ForEach(x =>
                {
                    var subItem = new GetAllWorkOrderDto
                    {
                        EmployeeId = x.EmployeeId,
                        WorkOrderId = x.Id,
                        TaskId = x.TaskId,
                        Status = (WorkOrderStatusType)x.StatusId,
                        IsOpen = x.IsOpen
                    };

                    if (x.WorkOrderDetail != null)
                    {
                        subItem.DetailsOfTask = x.WorkOrderDetail.DetailsOfTask;
                        subItem.WorkOrderDetailId = x.WorkOrderDetail.WorkOrderId;
                    }

                    responseItem.Add(subItem);
                });
            }

            return GenericResponse<List<GetAllWorkOrderDto>>.Sucess(responseItem, contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
