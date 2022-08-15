using FluentValidation.AspNetCore;
using ManageTaskAssignment.Employee.Api.Services;
using ManageTaskAssignment.Employee.Api.Validatiors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

#pragma warning disable CS0618 


builder.Services.AddControllers(opt=>
{
    opt.Filters.Add(new AuthorizeFilter());
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddEmployeeDtoValidation>());


#pragma warning restore CS0618 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.Authority = builder.Configuration["IdentityServerURL"];
    option.Audience = "resource_employee";
    option.RequireHttpsMetadata = false;
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
