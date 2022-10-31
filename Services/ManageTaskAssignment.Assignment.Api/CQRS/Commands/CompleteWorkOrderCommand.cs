using ManageTaskAssignment.SharedObjects;
using MediatR;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Commands
{
    public class CompleteWorkOrderCommand : IRequest<GenericResponse<NoContent>>
    {
        public Guid WorkOrderId { get; set; }
        public Guid EmployeeId { get; set; }
        public string DetailsOfTask { get; set; }
        public string UpdatedBy { get; set; }
    }
}
