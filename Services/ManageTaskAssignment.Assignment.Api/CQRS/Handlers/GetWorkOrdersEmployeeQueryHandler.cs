
using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class GetWorkOrdersEmployeeQueryHandler : IRequestHandler<GetWorkOrdersByEmployeeQuery, GenericResponse<List<GetWorkOrderDto>>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;

        private readonly IHttpContextAccessor contextAccessor;

        public GetWorkOrdersEmployeeQueryHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<List<GetWorkOrderDto>>> Handle(GetWorkOrdersByEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.EmployeeId)} can not be empty value !");
            }

            var responseItem = new List<GetWorkOrderDto>();

            var workOrdersByEmployee = await workOrderDbContext.WorkOrders.Include(x=> x.WorkOrderDetail).Where(x => x.EmployeeId == request.EmployeeId).ToListAsync(cancellationToken);

            if (workOrdersByEmployee.Any())
            {
                workOrdersByEmployee.ForEach(x =>
                {
                    var subItem = new GetWorkOrderDto
                    {
                        EmployeeId = x.EmployeeId,
                        WorkOrderId = x.Id,
                        TaskId = x.TaskId,
                        Status = (WorkOrderStatusType)x.StatusId,
                        IsOpen = x.IsOpen
                    };

                    if (!x.IsOpen)
                    {
                        if (x.WorkOrderDetail != null)
                        {
                            subItem.DetailsOfTask = x.WorkOrderDetail.DetailsOfTask;
                            subItem.WorkOrderDetailId = x.WorkOrderDetail.Id;
                        }
                    }

                    responseItem.Add(subItem);
                });
            }

            return GenericResponse<List<GetWorkOrderDto>>.Sucess(responseItem, contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
