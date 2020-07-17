using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Framework;

namespace Api.StartupExtensions
{
	public class NLogStartup
	{
		public static void SetNLogConfig(IAppConfig appConfig)
		{
			GlobalDiagnosticsContext.Set("defaultConnection", appConfig.WebApiExampleDbConnectionString);
			NLogBuilder.ConfigureNLog("nlog.config");
		}
	}
}
