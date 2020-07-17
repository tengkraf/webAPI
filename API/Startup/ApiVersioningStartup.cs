using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Api.StartupExtensions
{
	public static class ApiVersioningStartup
	{
		public static IServiceCollection AddApiVersioningStartup(this IServiceCollection services)
		{
			services.AddApiVersioning(o =>
			{
				o.ReportApiVersions = true;
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = new ApiVersion(1, 0);
				o.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

			services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

			return services;
		}
	}
}
