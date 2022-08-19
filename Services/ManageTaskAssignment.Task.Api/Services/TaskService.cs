
using T = System.Threading.Tasks;
using ManageTaskAssignment.Task.Api.Models;
using ManageTaskAssignment.Task.Api.Settings;
using MongoDB.Driver;
using ManageTaskAssignment.SharedObjects;

namespace ManageTaskAssignment.Task.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMongoCollection<TaskModel> taskModelCollection;

        private readonly IHttpContextAccessor contextAccessor;

        public TaskService(IMongoDbSetting mongoDbSetting, IHttpContextAccessor contextAccessor)
        {
            var client = new MongoClient(mongoDbSetting.ConnectionString);

            var database = client.GetDatabase(mongoDbSetting.DatabaseName);

            this.taskModelCollection = database.GetCollection<TaskModel>(mongoDbSetting.TaskCollectionName);

            this.contextAccessor = contextAccessor;
        }

        public async T.Task<GenericResponse<NoContent>> CreateAsync(TaskModel task)
        {
            try
            {
                await taskModelCollection.InsertOneAsync(task);

                return GenericResponse<NoContent>.Sucess(contextAccessor.HttpContext.Response.StatusCode);

            }
            catch (Exception ex)
            {
                return GenericResponse<NoContent>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }

        public async T.Task<GenericResponse<List<TaskModel>>> GetAllAsync()
        {
            try
            {
                var tasks = await taskModelCollection.Find(t => true).ToListAsync();

                return GenericResponse<List<TaskModel>>.Sucess(tasks, contextAccessor.HttpContext.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return GenericResponse<List<TaskModel>>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }

        }

        public async T.Task<GenericResponse<TaskModel>> GetByIdAsync(string id)
        {
            try
            {
                var taskItem = await taskModelCollection.Find<TaskModel>(t => t.Id == id).FirstAsync();

                return GenericResponse<TaskModel>.Sucess(taskItem, contextAccessor.HttpContext.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return GenericResponse<TaskModel>.Failed(ex.Message, contextAccessor.HttpContext.Response.StatusCode);
            }
        }
    }
}
