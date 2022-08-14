using ManageTaskAssignment.SharedObjects;

namespace ManageTaskAssignment.Employee.Api.Services
{
    public interface IEmployeeService
    {
        Task<GenericResponse<List<Models.Employee>>> GetAllEmployeeAsync();

        Task<GenericResponse<Models.Employee>> GetEmployeeByIdAsync(Guid id);

        Task<GenericResponse<NoContent>> AddEmployeeAsync(Dtos.EmployeeDto employee);
    }
}
