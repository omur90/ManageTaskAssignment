
using MongoDB.Bson.Serialization.Attributes;

namespace ManageTaskAssignment.Task.Api.Models
{
    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public string? Name { get; set; }
        
        public List<TaskItemModel>? TaskItems { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Boolean)]
        public bool IsActive { get; set; }  
    }
}
