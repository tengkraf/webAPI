using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
	public class OrderedItem : BaseEntity
	{
		public int OrderedItemId { get; set; }
		public int OrderId { get; set; }
		public int ItemId { get; set; }
		public int ItemCnt { get; set; }
		public Order Order { get; set; }
	}
}
