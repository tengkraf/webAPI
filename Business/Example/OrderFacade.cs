using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccess;
using DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.QueryObjects;
using DTO;
using Order = DataAccess.Entities.Order;
using System.Threading;
using Framework;
using DataMgmtService.Client;

namespace Business
{
	public class OrderFacade : IOrderFacade
	{
		private readonly IOrderDbContext _orderDbContext;
        private readonly IGenericValidator _genericValidator;
        private readonly IMapper _mapper;
        private readonly IDataMgmtServiceClient _dataMgmtServiceClient;

        public OrderFacade(
			IOrderDbContext orderDbContext,
            IGenericValidator genericValidator,
            IMapper mapper,
            IDataMgmtServiceClient dataMgmtServiceClient)
		{
			_orderDbContext = orderDbContext;
            _genericValidator = genericValidator;
            _mapper = mapper;
            _dataMgmtServiceClient = dataMgmtServiceClient;
        }

        public async Task<DTO.Order> GetById(int orderId)
        {
            // AutoMapper's ProjectTo(LINQ query) method maps properties from the database entity to the DTO.
            // Additionally it automatically adds a select statement to the LINQ query parameter 
            // which contains the columns needed to populate the DTO.
            return await _mapper
                .ProjectTo<DTO.Order>(
                    _orderDbContext.Order
                        .AsNoTracking()
                        .IncludeAllRelations()
                        .Where(x => x.OrderId == orderId))
                .FirstOrDefaultAsync();
        }

        public async Task<DTO.Order> GetByIdUsingStoredProc(int orderId)
        {
            List<OrderWithItemsAndAddressQuery> orderRecords =
                await _orderDbContext.OrderQuery.FromSql($"p_sel_order_by_id {orderId}").ToListAsync();

            if (!orderRecords.Any())
                return null;

            OrderWithItemsAndAddressQuery firstRecord = orderRecords.First();

            return new DTO.Order()
            {
                OrderId = firstRecord.OrderId,
                CustomerId = firstRecord.CustomerId,
                ShippingAddress = new ShippingAddress()
                {
                    ShippingAddressId = firstRecord.ShippingAddressId,
                    Addr1Text = firstRecord.Addr1Text,
                    Addr2Text = firstRecord.Addr2Text,
                    CityName = firstRecord.CityName,
                    CountryCode = firstRecord.CountryCode,
                    StateCode = firstRecord.StateCode,
                    ZipCode = firstRecord.ZipCode
                },
                OrderedItems = orderRecords.Select(orderRecord => new OrderedItem()
                {
                    OrderedItemId = orderRecord.OrderedItemId,
                    ItemCnt = orderRecord.ItemCnt,
                    ItemId = orderRecord.ItemId
                }).ToList()
            };
        }

        public async Task<PagedResult<DTO.Order>> GetByCustomerId(int customerId, int pageNum, int pageSize)
		{
            return await _mapper
                .ProjectTo<DTO.Order>(
                    _orderDbContext.Order
                        .AsNoTracking()
                        .IncludeAllRelations()
                        .Where(x => x.CustomerId == customerId))
                .ToPagedResultAsync(pageNum, pageSize);
		}

		public void AddOrder(DTO.Order order)
		{
			_orderDbContext.Order.Add(_mapper.Map<Order>(order));
		}

		public async Task UpdateOrder(DTO.Order order)
		{
			Order existingOrder = await _orderDbContext.Order
				.IncludeAllRelations()
				.FirstOrDefaultAsync(x => x.OrderId == order.OrderId);

            _genericValidator.ValidateEntityExists(existingOrder, order.OrderId);

            _mapper.Map(order, existingOrder);
		}

		public async Task DeleteOrder(int orderId)
		{
			Order existingOrder = await _orderDbContext.Order
				.IncludeAllRelations()
				.FirstOrDefaultAsync(x => x.OrderId == orderId);

            _genericValidator.ValidateEntityExists(existingOrder, orderId);

            _orderDbContext.ShippingAddress.Remove(existingOrder.ShippingAddress);
			_orderDbContext.OrderedItem.RemoveRange(existingOrder.OrderedItems);
			_orderDbContext.Order.Remove(existingOrder);
		}

        public async Task<bool> DoMetadataRecordsExist(bool showOnlyUnapprovedRecords)
        {
			ICollection<MetaDataElement> result =
				await _dataMgmtServiceClient.ViewMetadataAsync("", "", "", "", "", showOnlyUnapprovedRecords);

			return result.Count > 0;
        }
    }

	public interface IOrderFacade
    {
        Task<DTO.Order> GetById(int orderId);
        Task<DTO.Order> GetByIdUsingStoredProc(int orderId);
        Task<PagedResult<DTO.Order>> GetByCustomerId(int customerId, int pageNum, int pageSize);
		void AddOrder(DTO.Order order);
		Task UpdateOrder(DTO.Order order);
		Task DeleteOrder(int orderId);
        Task<bool> DoMetadataRecordsExist(bool showOnlyUnapprovedRecords);
    }
}
