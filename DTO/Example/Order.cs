using System;
using System.Collections.Generic;

namespace DTO
{
	public class Order
	{
		public Order()
		{
			OrderedItems = new List<OrderedItem>();
		}

		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public ShippingAddress ShippingAddress { get; set; }
		public List<OrderedItem> OrderedItems { get; set; }
	}
}
