
using ManageTaskAssignment.Task.Api.Services;
using ManageTaskAssignment.Task.Api.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHttpContextAccessor();

services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDbSettings"));

services.AddScoped<ITaskService, TaskService>();
services.AddSingleton<IMongoDbSetting>(mongoSetting =>
{
    return mongoSetting.GetRequiredService<IOptions<MongoDbSetting>>().Value;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
