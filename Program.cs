using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyBlog
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        // Add services here
                    })
					.Configure((hostContext, app) =>
                    {
                        if (hostContext.HostingEnvironment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseRouting();

                        // Configure middleware and endpoints here

                    });
					webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
				});
	}
}
