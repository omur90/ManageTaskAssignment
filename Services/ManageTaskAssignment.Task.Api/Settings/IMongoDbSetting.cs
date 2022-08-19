namespace ManageTaskAssignment.Task.Api.Settings
{
    public interface IMongoDbSetting
    {
        public string TaskCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
