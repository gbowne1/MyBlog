using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Models;
using MyBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        Console.WriteLine("Warning: DefaultConnection string is not configured. Falling back to the default connection string.");
        connectionString = "Server=(localdb)\\mssqllocaldb;Database=MyBlog;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
    options.UseSqlServer(connectionString);
});

// Configure Identity for authentication and authorization
// Only ONE call to AddDefaultIdentity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register custom services for Dependency Injection
builder.Services.AddTransient<IAboutContentService, AboutContentService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error != null)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(exceptionHandlerPathFeature.Error, "An unhandled exception occurred.");
            }
            await context.Response.WriteAsync("An error occurred. Redirecting to error page...");
            context.Response.Redirect("/Home/Error");
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Added UseAuthentication middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "reset-password",
    pattern: "password/reset/{token?}",
    defaults: new { controller = "ResetPassword", action = "Index" });

// Default route (fallback for unspecified controllers)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
