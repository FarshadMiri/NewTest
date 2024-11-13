using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestWithValue.Application.profile;
using TestWithValue.Data;
using TestWithValue.Data.SeedData;
using TestWithValue.Infrastructure.IOC;
using TestWithValue.Web.HubSupport;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceServices(builder.Configuration);
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddSignalR();
builder.WebHost.ConfigureKestrel(options =>
{
	options.AllowSynchronousIO = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSignalR();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<SupportHub>("/supportHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
