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
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                           ?? "Server=(localdb)\\mssqllocaldb;Database=MyBlog;Trusted_Connection=True;MultipleActiveResultSets=true";
    options.UseSqlServer(connectionString);
});

// Register custom services for Dependency Injection
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddTransient<IAboutContentService, AboutContentService>();

// Configure Identity for authentication and authorization
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Custom routes for specific controllers
app.MapControllerRoute(
    name: "about",
    pattern: "about",
    defaults: new { controller = "About", action = "Index" });

app.MapControllerRoute(
    name: "account",
    pattern: "account/{action}/{id?}",
    defaults: new { controller = "Account" });

app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{action}/{slug?}",
    defaults: new { controller = "Blog" });

app.MapControllerRoute(
    name: "changepassword",
    pattern: "change-password/{action}/{id?}",
    defaults: new { controller = "ChangePassword" });

app.MapControllerRoute(
    name: "comments",
    pattern: "comments/{action}/{id?}",
    defaults: new { controller = "Comment" });

app.MapControllerRoute(
    name: "editor",
    pattern: "editor/{action}/{id?}",
    defaults: new { controller = "Editor" });

app.MapControllerRoute(
    name: "external-login",
    pattern: "external-login/{action}/{id?}",
    defaults: new { controller = "ExternalLogin" });

app.MapControllerRoute(
    name: "manage",
    pattern: "manage/{action}/{id?}",
    defaults: new { controller = "Manage" });

app.MapControllerRoute(
    name: "register",
    pattern: "register/{action}/{id?}",
    defaults: new { controller = "Register" });

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
