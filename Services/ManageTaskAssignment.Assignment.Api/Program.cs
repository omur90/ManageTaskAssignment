
using ManageTaskAssignment.Assignment.Api;
using ManageTaskAssignment.Assignment.Api.CQRS.Handlers;
using ManageTaskAssignment.Assignment.Api.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region ThirdParty Service Register

services.AddControllers();
services.AddHttpContextAccessor();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#endregion

services.AddScoped<IWorkOrderService, WorkOrderService>();

#region DbContext Register And Configuration

services.AddDbContext<WorkOrderDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("ManageTaskAssignment.Assignment.Api");
    });
});

#endregion

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddSingleton<ManageTaskAssignment.SharedObjects.Services.ISharedIdentityService, ManageTaskAssignment.SharedObjects.Services.SharedIdentityService>();

#region JWT Configuration

services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = builder.Configuration["IdentityServerURL"];
    opt.Audience = "resource_assignment_api";
    opt.RequireHttpsMetadata = false;
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

services.AddAuthorization(option =>
{
    option.AddPolicy("ClientToken", policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "assignment_api_client_permission"));
    option.AddPolicy("UserToken", policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "assignment_api_user_permission"));
});


#endregion

#region CQRS Service Register

services.AddMediatR(typeof(GetWorkOrdersEmployeeQueryHandler).Assembly);
services.AddMediatR(typeof(GetAllWorkOrderQueryHandler).Assembly);
services.AddMediatR(typeof(GetWorkOrderByTaskQueryHandler).Assembly);
services.AddMediatR(typeof(CompleteWorkOrderCommandHandler).Assembly);
services.AddMediatR(typeof(CreateWorkOrderCommandHandler).Assembly);
services.AddMediatR(typeof(CancelWorkOrderCommandHandler).Assembly);

#endregion

#region Middleware Configurations

var app = builder.Build();

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

#endregion
