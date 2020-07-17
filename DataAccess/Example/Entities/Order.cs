using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
	public class Order : BaseEntity
	{
		public Order()
		{
			OrderedItems = new List<OrderedItem>();
		}

		public int OrderId { get; set; }
		public int ShippingAddressId { get; set; }
		public int CustomerId { get; set; }

		public ShippingAddress ShippingAddress { get; set; }
		public List<OrderedItem> OrderedItems { get; set; }
	}
}
