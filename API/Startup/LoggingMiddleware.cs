using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Framework;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using System.Net;
using Framework.Exceptions;
using Newtonsoft.Json;
using System.Globalization;

namespace Api.StartupExtensions
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private ILoggingService _loggingService;

		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext, ILoggingService loggingService)
		{
			try
			{
				_loggingService = loggingService;

				var request = httpContext.Request;

				if (request.Path.StartsWithSegments(new PathString("/api"), StringComparison.OrdinalIgnoreCase))
				{
					//TODO Look into using correlation Id to track requests across microservices
					_loggingService.LogRequest(httpContext.TraceIdentifier, request.Method, await ReadRequestBody(request));

					var originalBodyStream = httpContext.Response.Body;

					//TODO explain all of this
					using (var responseBody = new MemoryStream())
					{
						var response = httpContext.Response;
						response.Body = responseBody;

						try
						{
							await _next(httpContext);

							string responseBodyContent = await ReadResponseBody(response);
							await responseBody.CopyToAsync(originalBodyStream);

							_loggingService.LogResponse(httpContext.TraceIdentifier, request.Method, responseBodyContent);
						}
						catch (Exception ex)
						{
							string responseBodyContent = await ReadResponseBody(response);
							await responseBody.CopyToAsync(originalBodyStream);

							_loggingService.LogExceptionResponse(httpContext.TraceIdentifier, request.Method, responseBodyContent, ex);
							await HandleExceptionAsync(httpContext, ex);
						}
					}
				}
				else
				{
					await _next(httpContext);
				}
			}
			catch (Exception)
			{
				await _next(httpContext);
			}
		}

		private async Task<string> ReadRequestBody(HttpRequest request)
		{
			request.EnableRewind();

			var buffer = new byte[Convert.ToInt32(request.ContentLength, CultureInfo.InvariantCulture)];
			await request.Body.ReadAsync(buffer, 0, buffer.Length);
			var bodyAsText = Encoding.UTF8.GetString(buffer);
			request.Body.Seek(0, SeekOrigin.Begin);

			return bodyAsText;
		}

        // This warning is suppressed because disposing of the StreamRader here will close the underlying body stream at the conclusion 
        // of the using block and code later in the request lifecycle wont be able to read the body.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
        private async Task<string> ReadResponseBody(HttpResponse response)
		{
			response.Body.Seek(0, SeekOrigin.Begin);
			var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
			response.Body.Seek(0, SeekOrigin.Begin);

			return bodyAsText;
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

			return context.Response.WriteAsync(JsonConvert.SerializeObject(new
			{
				// customize as you need
				error = new
				{
					message = exception.Message,
					exception = exception.GetType().Name
				}
			}));
		}
	}
}
