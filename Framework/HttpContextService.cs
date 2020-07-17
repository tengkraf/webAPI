using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	public class HttpContextService : IHttpContextService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HttpContextService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUserName()
		{
			return _httpContextAccessor.HttpContext.User.Identity.Name;
		}

		public string GetTraceIdentifier()
		{
			return _httpContextAccessor.HttpContext.TraceIdentifier;
		}
	}

	public interface IHttpContextService
	{
		string GetUserName();
		string GetTraceIdentifier();
	}
}
