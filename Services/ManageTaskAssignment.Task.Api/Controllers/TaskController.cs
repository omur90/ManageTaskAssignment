using ManageTaskAssignment.SharedObjects;
using ManageTaskAssignment.Task.Api.Models;
using ManageTaskAssignment.Task.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageTaskAssignment.Task.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : CustomBaseController
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = TokenConstants.UserTokenPolicy, Roles = TokenConstants.Member)]
        public async Task<IActionResult> GetById(string id)
            => CreateActionResult<TaskModel>(await taskService.GetByIdAsync(id));


        [HttpGet]
        [Authorize(Policy = TokenConstants.UserTokenPolicy, Roles = TokenConstants.Admin)]
        public async Task<IActionResult> GetAll()
            => CreateActionResult<List<TaskModel>>(await taskService.GetAllAsync());

        [HttpPost]
        [Authorize(Policy = TokenConstants.UserTokenPolicy, Roles = TokenConstants.Admin)]
        public async Task<IActionResult> Create(TaskModel taskModel)
            => CreateActionResult(await taskService.CreateAsync(taskModel));
    }
}
