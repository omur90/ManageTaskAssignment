using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Queries
{
    public class GetWorkOrdersByEmployeeQuery : IRequest<GenericResponse<List<GetWorkOrderDto>>>
    {
        public Guid EmployeeId { get; set; } 
    }
}
