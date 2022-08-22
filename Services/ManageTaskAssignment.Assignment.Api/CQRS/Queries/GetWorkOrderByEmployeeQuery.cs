using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Queries
{
    public class GetWorkOrderByEmployeeQuery : IRequest<GenericResponse<List<GetWorkOrderByEmployeeDto>>>
    {
        public Guid EmployeeId { get; set; } 
    }
}
