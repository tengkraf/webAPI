using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Api.StartupExtensions
{
	public static class SwaggerStartup
	{
		public static IServiceCollection AddSwaggerStartup(this IServiceCollection services)
		{
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			services.AddSwaggerGen(c =>
			{
				//c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
					$"{Assembly.GetExecutingAssembly().GetName().Name}.XML"));
			});

			return services;
		}

		public static IApplicationBuilder UseSwaggerStartup(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(options =>
			{
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerEndpoint(
						$"/swagger/{description.GroupName}/swagger.json",
					   $"Account API {description.GroupName.ToUpperInvariant()}");
				}
				//options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				options.RoutePrefix = string.Empty;
			});

			return app;
		}
	}

	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		readonly IApiVersionDescriptionProvider provider;

		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
		  this.provider = provider;

		public void Configure(SwaggerGenOptions options)
		{
			foreach (var description in provider.ApiVersionDescriptions)
			{
				options.SwaggerDoc(
				  description.GroupName,
					new Info()
					{
						Title = $"API {description.ApiVersion}",
						Version = description.ApiVersion.ToString(),
					});
			}
		}
	}
}
