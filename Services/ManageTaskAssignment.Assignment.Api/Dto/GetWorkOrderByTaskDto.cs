using Microsoft.AspNetCore.Identity;

namespace ManageTaskAssignment.Assignment.Api.Dto
{
    public class GetWorkOrderByTaskDto
    {
        public Guid TaskId { get; set; }
    }
}
