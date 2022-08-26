using FluentValidation.AspNetCore;
using ManageTaskAssignment.Employee.Api.Services;
using ManageTaskAssignment.Employee.Api.Validatiors;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

#pragma warning disable CS0618 

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddEmployeeDtoValidation>());

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
{
    option.Authority = builder.Configuration["IdentityServerURL"];
    option.Audience = "resource_employee_api";
    option.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("ClientToken", policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "employee_api_client_permission"));
    option.AddPolicy("UserToken", policy => policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireClaim("scope", "employee_api_user_permission"));
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
