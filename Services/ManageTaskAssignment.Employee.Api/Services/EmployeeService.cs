
using Dapper;
using ManageTaskAssignment.Employee.Api.Mapper;
using ManageTaskAssignment.SharedObjects;
using Npgsql;
using System.Data;

namespace ManageTaskAssignment.Employee.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;

        private readonly IDbConnection _dbConnection;

        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;

            _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<GenericResponse<NoContent>> AddEmployeeAsync(Dtos.EmployeeDto employeeDto)
        {
            try
            {
                var employee = ObjectMapper.Mapper.Map<Models.Employee>(employeeDto);

                var executeCount = await _dbConnection.ExecuteAsync("INSERT INTO employee (userid,name,surname,phonenumber,expireworkdate) VALUES (@UserId,@Name,@SurName,@PhoneNumber,@ExpireWorkDate)", employee);

                if (executeCount <= default(int))
                {
                    throw new Exception("Employee can not added");
                }

                return GenericResponse<NoContent>.Sucess(_contextAccessor.HttpContext.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return GenericResponse<NoContent>.Failed(ex.Message, _contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async Task<GenericResponse<List<Models.Employee>>> GetAllEmployeeAsync()
        {
            try
            {
                var employees = await _dbConnection.QueryAsync<Models.Employee>("SELECT * FROM employee");

                return GenericResponse<List<Models.Employee>>.Sucess(employees.ToList(), _contextAccessor.HttpContext.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return GenericResponse<List<Models.Employee>>.Failed(ex.Message, _contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async Task<GenericResponse<Models.Employee>> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var employee = (await _dbConnection.QueryAsync<Models.Employee>("SELECT * FROM employee Where id = @Id", new { Id = id })).SingleOrDefault();

                if (employee == null || employee == default)
                {
                    throw new Exception("Employee not found");
                }

                return GenericResponse<Models.Employee>.Sucess(employee, _contextAccessor.HttpContext.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return GenericResponse<Models.Employee>.Failed(ex.Message, _contextAccessor.HttpContext.Response.StatusCode);
            }
        }
    }
}
