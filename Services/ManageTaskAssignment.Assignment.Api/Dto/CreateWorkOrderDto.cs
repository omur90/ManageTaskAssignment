using ManageTaskAssignment.Assignment.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace ManageTaskAssignment.Assignment.Api.Dto
{
    public class CreateWorkOrderDto
    {
        public Guid EmployeeId { get; set; }
        public string TaskId { get; set; }
    }
}
