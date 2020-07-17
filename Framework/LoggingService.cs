using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	public class LoggingService : ILoggingService
	{
		public const string RequestMessageType = "Request";
		public const string ResponseMessageType = "Response";

		private readonly Logger _logger;
		private readonly IHttpContextService _httpContextService;

		public LoggingService(IHttpContextService httpContextService)
		{
			_httpContextService = httpContextService;
			_logger = LogManager.GetCurrentClassLogger();
		}

		public void LogRequest(string requestId, string requestMethod, string requestContent)
		{
			Log(requestId, requestMethod, requestContent ?? "Ommitted content", RequestMessageType, LogLevel.Info);
		}

		public void LogResponse(string requestId, string requestMethod, string responseContent)
		{
			Log(requestId, requestMethod, responseContent ?? "Ommitted content", ResponseMessageType, LogLevel.Info);
		}

		public void LogExceptionResponse(string requestId, string requestMethod, string responseContent, Exception exception)
		{
			Log(requestId, requestMethod, responseContent ?? "Ommitted content", ResponseMessageType, LogLevel.Fatal, exception);
		}

		private void Log(string requestId, string requestMethod, string message, string messageType, LogLevel logLevel, Exception exception = null)
		{
			var logEvent = new LogEventInfo() { Level = logLevel, Message = message };
			logEvent.Properties.Add("RequestID", requestId);
			logEvent.Properties.Add("RequestMethod", requestMethod);
			logEvent.Properties.Add("UserName", _httpContextService.GetUserName());
			logEvent.Properties.Add("MessageType", messageType);

			if (exception != null)
			{
				logEvent.Exception = exception;
			}

			_logger.Log(logEvent);
		}
	}

	public interface ILoggingService
	{
		void LogRequest(string requestId, string requestMethod, string requestContent);
		void LogResponse(string requestId, string requestMethod, string responseContent);
		void LogExceptionResponse(string requestId, string requestMethod, string responseContent, Exception exception);
	}
}
