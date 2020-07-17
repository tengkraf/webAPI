using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace Api.Controllers.V1
{
	/// <summary>
	/// The 1st version of the Order controller.
	/// </summary>
	[ApiVersion("1.0")]
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderFacade _orderFacade;
		private readonly IOrderDbContext _orderDbContext;

		public OrderController(IOrderFacade orderFacade,
            IOrderDbContext orderDbContext)
		{
			_orderFacade = orderFacade;
			_orderDbContext = orderDbContext;
		}

        [HttpGet("{orderId}")]
		public async Task<ActionResult<Order>> Get(int orderId)
		{
			return Ok(await _orderFacade.GetById(orderId));
		}

        [HttpGet("GetUsingStoredProc/{orderId}")]
        public async Task<ActionResult<Order>> GetUsingStoredProc(int orderId)
        {
            return Ok(await _orderFacade.GetByIdUsingStoredProc(orderId));
        }

        [HttpGet("GetByCustomerId/{customerId}/{pageNum}/{pageSize}")]
		public async Task<ActionResult<List<Order>>> GetByCustomerId(int customerId, int pageNum, int pageSize)
		{
			return Ok(await _orderFacade.GetByCustomerId(customerId, pageNum, pageSize));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Order order)
		{
			_orderFacade.AddOrder(order);
			await _orderDbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPut()]
		public async Task<IActionResult> Put([FromBody] Order order)
		{
			await _orderFacade.UpdateOrder(order);
			await _orderDbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _orderFacade.DeleteOrder(id);
			await _orderDbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
