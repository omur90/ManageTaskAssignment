namespace ManageTaskAssignment.Task.Api.Settings
{
    public class MongoDbSetting : IMongoDbSetting
    {
        public string? TaskCollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
