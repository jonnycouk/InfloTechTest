using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Mapper;
using UserManagement.Api.Services;
using UserManagement.Data;
using UserManagement.Services.Domain.Implementations;
using UserManagement.Services.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<LogMapper>();
    
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/openapi/v1.json", "v1");
//     });
// }

// For Live Demo

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});



app.MapControllers();

app.Run();