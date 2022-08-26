using AutoMapper;
using ManageTaskAssignment.Assignment.Api.CQRS.Queries;
using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Handlers
{
    public class GetWorkOrderByTaskQueryHandler : IRequestHandler<GetWorkOrderByTaskQuery, GenericResponse<GetWorkOrderDto>>
    {
        private readonly WorkOrderDbContext workOrderDbContext;

        private readonly IHttpContextAccessor contextAccessor;

        private readonly IMapper mapper;

        public GetWorkOrderByTaskQueryHandler(WorkOrderDbContext workOrderDbContext, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            this.workOrderDbContext = workOrderDbContext;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }

        public async Task<GenericResponse<GetWorkOrderDto>> Handle(GetWorkOrderByTaskQuery request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.EmployeeId)} can not be empty value !");
            }

            if (request.TaskId == Guid.Empty)
            {
                throw new CustomBusinessException($"{nameof(request.TaskId)} can not be empty value !");
            }

            var workOrderByTask = await workOrderDbContext.WorkOrders.Where(x => x.EmployeeId == request.EmployeeId && x.TaskId == $"{request.TaskId}").FirstOrDefaultAsync(cancellationToken);

            if (workOrderByTask == null)
            {
                throw new CustomBusinessException($"{nameof(workOrderByTask)} can not be null !");
            }

            return GenericResponse<GetWorkOrderDto>.Sucess(mapper.Map<GetWorkOrderDto>(workOrderByTask), contextAccessor.HttpContext.Response.StatusCode);
        }
    }
}
