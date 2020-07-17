using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Api.StartupExtensions
{
	public static class HttpsStartup
	{
		public static IServiceCollection AddHttpsStartup(this IServiceCollection services)
		{
			services.AddHsts(options =>
			{
				// Read Hsts info here: https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio#http-strict-transport-security-protocol-hsts
				//options.Preload = true;
				//options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromHours(12); // Change to longer value once confirmed working in PROD.
				//options.ExcludedHosts.Add("example.com");
			});

			return services;
		}
	}
}
