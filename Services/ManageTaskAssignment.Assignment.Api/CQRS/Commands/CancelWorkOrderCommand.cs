
using ManageTaskAssignment.SharedObjects;
using MediatR;
using System.Diagnostics;

namespace ManageTaskAssignment.Assignment.Api.CQRS.Commands
{
    public class CancelWorkOrderCommand : IRequest<GenericResponse<NoContent>>
    {
        public Guid WorkOrderId { get; set; }
        public Guid EmployeeId { get; set; }
        public string UpdatedBy { get; set; }

    }
}
