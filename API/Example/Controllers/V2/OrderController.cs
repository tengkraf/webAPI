using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V2
{
	/// <summary>
	/// The 2nd version of the Order controller, better, faster, stronger!
	/// </summary>
	[ApiVersion("2.0")]
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
        private readonly IOrderFacade _orderFacade;

        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> Get(int orderId)
        {
            await _orderFacade.GetById(orderId);

            return Ok();
        }
    }
}
