using ManageTaskAssignment.Assignment.Api.Dto;
using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Queries
{
    public class GetWorkOrderByTaskQuery : IRequest<GenericResponse<GetWorkOrderDto>>
    {
        public Guid EmployeeId { get; set; }
        public Guid TaskId { get; set; }
    }
}
