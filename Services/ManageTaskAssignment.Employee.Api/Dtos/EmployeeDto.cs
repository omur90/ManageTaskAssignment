namespace ManageTaskAssignment.Employee.Api.Dtos
{
    public class EmployeeDto
    {
        public string? EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime ExpireWorkDate { get; set; }
    }
}
