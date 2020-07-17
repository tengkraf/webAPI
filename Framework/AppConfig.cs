using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	public class AppConfig : IAppConfig
	{
		private readonly IConfiguration _configuration;

		public AppConfig(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		//
		public string EnvironmentString { get; set; }

		public string WebApiExampleDbConnectionString { get { return _configuration["ConnectionStrings:WebApiExampleDb"]; } }
		public string ExampleServiceEndPoint { get { return _configuration["ServiceEndPoint:ExampleServiceEndpoint"]; } }
	}

	public interface IAppConfig
	{
		string EnvironmentString { get; set; }
		string WebApiExampleDbConnectionString { get; }
		string ExampleServiceEndPoint { get; }
	}
}
