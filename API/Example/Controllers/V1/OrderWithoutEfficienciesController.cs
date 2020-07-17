using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Framework;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Http.Internal;

namespace Api.Controllers.V1
{
	/// <summary>
	/// The 1st version of the Order controller.
	/// </summary>
	[ApiVersion("1.0")]
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrderWithoutEfficienciesController : ControllerBase
	{
		private readonly IOrderFacade _orderFacade;
		private readonly IOrderDbContext _orderDbContext;
		private readonly ILoggingService _loggingService;
		private readonly IHttpContextService _httpContextService;

		public OrderWithoutEfficienciesController(IOrderFacade orderFacade,
            IOrderDbContext orderDbContext,
			ILoggingService loggingService,
			IHttpContextService httpContextService)
		{
			_orderFacade = orderFacade;
			_orderDbContext = orderDbContext;
			_loggingService = loggingService;
			_httpContextService = httpContextService;
		}

		// Under construction

  //      [HttpGet("{orderId}")]
		//public async Task<ActionResult<Order>> Get(int orderId)
		//{
		//	string test = "hello";

		//	string temp = test.ToLower().PadLeft(10, 'X');

		//	//try
		//	//{
		//	//	_loggingService.LogRequest(_httpContextService.GetTraceIdentifier(), Request.Method, await ReadRequestBody(Request));

		//	//	var response = Ok(await _orderFacade.GetById(orderId));

		//	//	_loggingService.LogResponse(_httpContextService.GetTraceIdentifier(), Request.Method, await ReadResponseBody(response));
		//	//}
		//	//catch (Exception e)
		//	//{

				
		//	//	_loggingService.LogExceptionResponse(_httpContextService.GetTraceIdentifier(), Request.Method, responseBodyContent, ex);
		//	//}
		//	//finally
		//	//{

		//	//}

		//	return Ok(await _orderFacade.GetById(orderId));
		//}

  //      [HttpGet("GetUsingStoredProc/{orderId}")]
  //      public async Task<ActionResult<Order>> GetUsingStoredProc(int orderId)
  //      {
  //          return Ok(await _orderFacade.GetByIdUsingStoredProc(orderId));
  //      }

		//[HttpGet("GetByCustomerId/{customerId}/{pageNum}/{pageSize}")]
		//public async Task<ActionResult<List<Order>>> GetByCustomerId(int customerId, int pageNum, int pageSize)
		//{
		//	return Ok(await _orderFacade.GetByCustomerId(customerId, pageNum, pageSize));
		//}

		//[HttpPost]
		//public async Task<IActionResult> Post([FromBody] Order order)
		//{
		//	_orderFacade.AddOrder(order);
		//	await _orderDbContext.SaveChangesAsync();
		//	return Ok();
		//}

		//[HttpPut()]
		//public async Task<IActionResult> Put([FromBody] Order order)
		//{
		//	await _orderFacade.UpdateOrder(order);
		//	await _orderDbContext.SaveChangesAsync();
		//	return Ok();
		//}

		//[HttpDelete("{id}")]
		//public async Task<IActionResult> Delete(int id)
		//{
		//	await _orderFacade.DeleteOrder(id);
		//	await _orderDbContext.SaveChangesAsync();
		//	return Ok();
		//}

		//private async Task<string> ReadRequestBody(HttpRequest request)
		//{
		//	request.EnableRewind();

		//	var buffer = new byte[Convert.ToInt32(request.ContentLength, CultureInfo.InvariantCulture)];
		//	await request.Body.ReadAsync(buffer, 0, buffer.Length);
		//	var bodyAsText = Encoding.UTF8.GetString(buffer);
		//	request.Body.Seek(0, SeekOrigin.Begin);

		//	return bodyAsText;
		//}

		//// This warning is suppressed because disposing of the StreamRader here will close the underlying body stream at the conclusion 
		//// of the using block and code later in the request lifecycle wont be able to read the body.
		//[System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "<Pending>")]
		//private async Task<string> ReadResponseBody(HttpResponse response)
		//{
		//	response.Body.Seek(0, SeekOrigin.Begin);
		//	var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
		//	response.Body.Seek(0, SeekOrigin.Begin);

		//	return bodyAsText;
		//}

	}
}
