using Dapper.Contrib.Extensions;

namespace ManageTaskAssignment.Employee.Api.Models
{
    [Table("employee")]
    public class Employee
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime ExpireWorkDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
