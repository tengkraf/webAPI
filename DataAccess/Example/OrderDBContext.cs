using DataAccess.Entities;
using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.QueryObjects;

namespace DataAccess
{
	public class OrderDbContext : DbContext, IOrderDbContext
	{
        private readonly IHttpContextService _httpContextService;

		public virtual DbSet<ShippingAddress> ShippingAddress { get; set; }
		public virtual DbSet<Order> Order { get; set; }
		public virtual DbSet<OrderedItem> OrderedItem { get; set; }
        public virtual DbQuery<OrderWithItemsAndAddressQuery> OrderQuery { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) {}

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IHttpContextService httpContextService) : base(options)
		{
            _httpContextService = httpContextService;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.HasDefaultSchema("order");

			// Replace table names

			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.Relational().TableName = ConvertFromPascalToLowerCaseUnderscoreName(entity.Relational().TableName);

				foreach (var property in entity.GetProperties())
				{
					property.Relational().ColumnName = ConvertFromPascalToLowerCaseUnderscoreName(property.Relational().ColumnName);
					property.IsUnicode(false);
				}
			}
		}

		private string ConvertFromPascalToLowerCaseUnderscoreName(string name)
		{
			var result = Regex.Replace(name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

			return result.ToLower();
		}
		public override int SaveChanges()
		{
			string userLogonId = _httpContextService.GetUserName();
			DateTime currentDateTime = DateTime.Now;

			ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity).ToList().ForEach(x =>
			{
				(x.Entity as BaseEntity).CreateLogonId = userLogonId;
				(x.Entity as BaseEntity).CreateDate = currentDateTime;
			});

			ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity).ToList().ForEach(x =>
			{
				(x.Entity as BaseEntity).LastUpdateLogonId = userLogonId;
				(x.Entity as BaseEntity).LastUpdateDate = currentDateTime;
			});

			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			string userLogonId = _httpContextService.GetUserName();
			DateTime currentDateTime = DateTime.Now;

			ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity).ToList().ForEach(x =>
			{
				(x.Entity as BaseEntity).CreateLogonId = userLogonId;
				(x.Entity as BaseEntity).CreateDate = currentDateTime;
			});

			ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity).ToList().ForEach(x =>
			{
				(x.Entity as BaseEntity).LastUpdateLogonId = userLogonId;
				(x.Entity as BaseEntity).LastUpdateDate = currentDateTime;
			});

			return await base.SaveChangesAsync();
		}
    }
}
