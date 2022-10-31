
using ManageTaskAssignment.SharedObjects;
using ManageTaskAssignment.Task.Api.Services;
using ManageTaskAssignment.Task.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers(opt=>
{
    opt.Filters.Add(new AuthorizeFilter());
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddHttpContextAccessor();

services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoDbSettings"));

services.AddScoped<ITaskService, TaskService>();
services.AddSingleton<IMongoDbSetting>(mongoSetting =>
{
    return mongoSetting.GetRequiredService<IOptions<MongoDbSetting>>().Value;
});

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.Authority = builder.Configuration["IdentityServerURL"];
    option.Audience = "resource_task_api";
    option.RequireHttpsMetadata = false; 
});

services.AddAuthorization(option =>
{
    option.AddPolicy(TokenConstants.UserTokenPolicy, policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "task_api_user_permission"));
    option.AddPolicy(TokenConstants.ClientTokenPolicy, policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "task_api_client_permission"));
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

app.UseAuthentication();

app.MapControllers();

app.Run();
