
using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.Assignment.Api.Enums;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class GetWorkOrderEmployeeQueryHandler : IRequestHandler<GetWorkOrderByEmployeeQuery, GenericResponse<List<GetWorkOrderByEmployeeDto>>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;

        private readonly IHttpContextAccessor contextAccessor;

        public GetWorkOrderEmployeeQueryHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<GenericResponse<List<GetWorkOrderByEmployeeDto>>> Handle(GetWorkOrderByEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(request.EmployeeId)} can not be empty value !");
            }

            var responseItem = new List<GetWorkOrderByEmployeeDto>();

            var workOrdersByEmployee = await workOrderDbContext.WorkOrders.Where(x => x.EmployeeId == request.EmployeeId).ToListAsync(cancellationToken);

            workOrdersByEmployee.ForEach(async x =>
            {
                var subItem = new GetWorkOrderByEmployeeDto
                {
                    EmployeeId = x.EmployeeId,
                    WorkOrderId = x.Id,
                    TaskId = x.TaskId,
                    Status = (WorkOrderStatusType)x.StatusId,
                    IsOpen = x.IsOpen
                };

                if (!x.IsOpen)
                {
                    var workOrderDetail = await workOrderDbContext.WorkOrderDetails.Where(x => x.WorkOrderId == x.Id).FirstOrDefaultAsync(cancellationToken);
                    if (workOrderDetail != null)
                    {
                        subItem.DetailsOfTask = workOrderDetail.DetailsOfTask;
                        subItem.WorkOrderDetailId = workOrderDetail.Id;
                    }
                }

                responseItem.Add(subItem);
            });

            return GenericResponse<List<GetWorkOrderByEmployeeDto>>.Sucess(responseItem, contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
