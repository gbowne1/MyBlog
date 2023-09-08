using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Models;

namespace MyBlog
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

            app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllerRoute(
						name: "default",
						pattern: "{controller=Home}/{action=Index}/{id?}");
				});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/blog/posts", async context =>
				{
					// Logic to retrieve a list of all blog posts
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Retrieved all blog posts");
				});

				endpoints.MapGet("/blog/posts/{id}", async context =>
				{
					// Logic to retrieve a specific blog post by its ID
					var postId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Retrieved blog post with ID: {postId}");
				});

				endpoints.MapPost("/blog/posts", async context =>
				{
					// Logic to create a new blog post
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Created a new blog post");
				});

				endpoints.MapPut("/blog/posts/{id}", async context =>
				{
					// Logic to update an existing blog post
					var postId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Updated blog post with ID: {postId}");
				});

				endpoints.MapDelete("/blog/posts/{id}", async context =>
				{
					// Logic to delete a blog post by its ID
					var postId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Deleted blog post with ID: {postId}");
				});

				// Blog categories endpoints
				endpoints.MapGet("/blog/categories", async context =>
				{
					// Logic to retrieve a list of all blog post categories
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Retrieved all blog post categories");
				});

				endpoints.MapGet("/blog/categories/{id}", async context =>
				{
					// Logic to retrieve a specific blog post category by its ID
					var categoryId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Retrieved blog post category with ID: {categoryId}");
				});

				endpoints.MapPost("/blog/categories", async context =>
				{
					// Logic to create a new blog post category
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Created a new blog post category");
				});

				endpoints.MapPut("/blog/categories/{id}", async context =>
				{
					// Logic to update an existing blog post category
					var categoryId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Updated blog post category with ID: {categoryId}");
				});

				endpoints.MapDelete("/blog/categories/{id}", async context =>
				{
					// Logic to delete a blog post category by its ID
					var categoryId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Deleted blog post category with ID: {categoryId}");
				});

				// Blog tags endpoints
				endpoints.MapGet("/blog/tags", async context =>
				{
					// Logic to retrieve a list of all blog post tags
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Retrieved all blog post tags");
				});

				endpoints.MapGet("/blog/tags/{id}", async context =>
				{
					// Logic to retrieve a specific blog post tag by its ID
					var tagId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Retrieved blog post tag with ID: {tagId}");
				});

				endpoints.MapPost("/blog/tags", async context =>
				{
					// Logic to create a new blog post tag
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync("Created a new blog post tag");
				});

				endpoints.MapPut("/blog/tags/{id}", async context =>
				{
					// Logic to update an existing blog post tag
					var tagId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Updated blog post tag with ID: {tagId}");
				});

				endpoints.MapDelete("/blog/tags/{id}", async context =>
				{
					var tagId = context.Request.RouteValues["id"];
					await Task.Delay(1000); // Example of an asynchronous operation
					await context.Response.WriteAsync($"Deleted blog post tag with ID: {tagId}");
				});
			});
		}
	}
}
