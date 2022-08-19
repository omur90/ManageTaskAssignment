
using ManageTaskAssignment.Task.Api.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace ManageTaskAssignment.Task.Api.Models
{
    public class TaskItemModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public TaskItemType TaskItemType { get; set; }
        
        public string? LabelName { get; set; }
        
        public string? ToolTip { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Boolean)]
        public bool IsRequired { get; set; }
        
        public string? Value { get; set; }
    }
}
