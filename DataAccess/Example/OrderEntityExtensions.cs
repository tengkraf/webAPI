using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
	public static class OrderEntityExtensions
	{
		public static IQueryable<Order> IncludeAllRelations(this IQueryable<Order> value)
		{
			return value
				.Include(x => x.ShippingAddress)
				.Include(x => x.OrderedItems);
		}
	}
}
