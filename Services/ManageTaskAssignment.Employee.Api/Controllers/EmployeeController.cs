
using ManageTaskAssignment.Employee.Api.Services;
using ManageTaskAssignment.SharedObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageTaskAssignment.Employee.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : CustomBaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Policy = TokenConstants.ClientTokenPolicy)]
        public async Task<IActionResult> GetAll()
            => CreateActionResult<List<Models.Employee>>(await _employeeService.GetAllEmployeeAsync());

        [HttpGet("{id}")]
        [Authorize(Policy = TokenConstants.ClientTokenPolicy)]
        public async Task<IActionResult> GetById(string id)
            => CreateActionResult<Models.Employee>(await _employeeService.GetEmployeeByIdAsync(Guid.Parse(id)));
 
        [HttpPost]
        [Authorize(Policy = TokenConstants.UserTokenPolicy, Roles = TokenConstants.Admin)]
        public async Task<IActionResult> Add(Dtos.EmployeeDto employee)
        {
            #region FluentValidationHandleErrors
            //Bu şekilde hataları yakalyarak response model oluşturabiliriz. Program.cs de ki builder.Services.AddControllers().AddFluentValidation(); register kullanılmalı.
            //var validator = new AddEmployeeDtoValidation();
            //var results = validator.Validate(employee);

            //results.AddToModelState(ModelState, null);

            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage);
            //}
            #endregion

            return CreateActionResult(await _employeeService.AddEmployeeAsync(employee));
        }
    }
}
