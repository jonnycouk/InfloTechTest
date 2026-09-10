using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.ApiServices.Logs;
using UserManagement.ApiServices.Users;
using UserManagement.Data;
using UserManagement.Web.Mapper;
using Westwind.AspNetCore.Markdown;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDataAccess()
    .AddDomainServices()
    .AddMarkdown()
    .AddControllersWithViews();

builder.Services.AddSingleton<UserMapper>();
builder.Services.AddSingleton<LogMapper>();
builder.Services.AddScoped<ILogApiService, LogApiService>();
builder.Services.AddScoped<IUserApiService, UserApiService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

var app = builder.Build();

app.UseMarkdown();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
