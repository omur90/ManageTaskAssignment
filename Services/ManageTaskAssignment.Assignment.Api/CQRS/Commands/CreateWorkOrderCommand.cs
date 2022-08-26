using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Commands
{
    public class CreateWorkOrderCommand : IRequest<GenericResponse<NoContent>>
    {
        public Guid EmployeeId { get; set; }
        public string TaskId { get; set; }
        public string CreatedBy { get; set; }
    }
}
