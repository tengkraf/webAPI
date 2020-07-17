using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.QueryObjects;

namespace DataAccess
{
	public interface IOrderDbContext : IDisposable
	{
        DbSet<ShippingAddress> ShippingAddress { get; set; }
		DbSet<Order> Order { get; set; }
		DbSet<OrderedItem> OrderedItem { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        DbQuery<OrderWithItemsAndAddressQuery> OrderQuery { get; set; }
    }
}
