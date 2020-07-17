using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;
using NLog.Web;

namespace Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				CreateWebHostBuilder(args).Build().Run();
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Shutdown();
			}	
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

			WebHost.CreateDefaultBuilder(args)
			.ConfigureServices(services => services.AddAutofac()) // Use Autofac as the IOC container
			.UseStartup<Startup>()
			.ConfigureLogging(logging => logging.ClearProviders()) // Clear the default logging providers because we will use NLog.
			.UseNLog(); // Use NLog for logging
	}
}
