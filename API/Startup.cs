using System;
using Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.StartupExtensions;
using System.Net.Http;
using DataMgmtService.Client;

namespace Api
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
        {
            Configuration = ConfigurationStartup.SetupConfiguration(env);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			// opt in or out of breaking changes in a specific asp.net core version
			// https://docs.microsoft.com/en-us/aspnet/core/mvc/compatibility-version?view=aspnetcore-2.2
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddHttpContextAccessor(); // Allows classes to access HttpContext via DI with IHttpContextAccessor
			services.AddApiVersioningStartup(); // Addslni API versioning (v1, v2) functionality
			services.AddSwaggerStartup(); // Swagger provides interactive API exploration and execution
			services.AddHttpsStartup(); // Configures Https and Hsts

			services.AddHttpClient<IDataMgmtServiceClient, DataMgmtServiceClient>
				(c => c.BaseAddress = new Uri(Configuration["ServiceEndPoint:ExampleServiceEndpoint"]))
			 .ConfigurePrimaryHttpMessageHandler(() =>
			 {
				 return new HttpClientHandler()
				 {
					 AllowAutoRedirect = false,
					 UseDefaultCredentials = true
				 };
			 });

			// Create the serviceProvider so that automapper startup can get the entity framework DB Model metadata.
			services.AddAutoMapperStartup(AutoFacStartup.CreateServiceProvider(services, Configuration["ConnectionStrings:WebApiExampleDb"]));

			// Create the service provider again so that it includes the AutoMapper in its services.
			return AutoFacStartup.CreateServiceProvider(services, Configuration["ConnectionStrings:WebApiExampleDb"]);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, IAppConfig appConfig)
		{
			appConfig.EnvironmentString = env.EnvironmentName;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Provides detailed error info *** Don't enable in PROD!!!!!! ***
            }
            else
            {
                app.UseHsts(); // send HTTP Strict Transport Security Protocol (HSTS) headers to clients.
			}

			NLogStartup.SetNLogConfig(appConfig); // Setup NLog Config and connection string
			app.UseSwaggerStartup(provider); // Swagger provides interactive API exploration and execution
			app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS.

			app.UseMiddleware<LoggingMiddleware>();

			app.UseMvc(); // Configures MVC routing middleware
		}
	}
}
