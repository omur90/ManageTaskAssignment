
using T = System.Threading.Tasks;
using ManageTaskAssignment.Task.Api.Models;
using ManageTaskAssignment.SharedObjects;

namespace ManageTaskAssignment.Task.Api.Services
{
    public interface ITaskService
    {
        T.Task<GenericResponse<NoContent>> CreateAsync(TaskModel task);
        T.Task<GenericResponse<List<TaskModel>>> GetAllAsync();
        T.Task<GenericResponse<TaskModel>> GetByIdAsync(string id);
    }
}
