using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Business;
using DataAccess;
using DTO;
using Framework;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class OrderFacadeITests : BaseIntegrationTest
    {
        private IOrderFacade _orderFacade;
        private OrderDbContext _orderDbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            base.CreateMockerWithInMemoryDB();
            base.SetupAutoFixtureWhichIgnoresIdFields();

            _orderFacade = Mocker.Create<OrderFacade>();
            _orderDbContext = Mocker.Create<OrderDbContext>();
        }

        [TestMethod]
        public async Task AddOrder_OrderIsAdded()
        {
            var _orderFacade = Mocker.Create<OrderFacade>();
            var _orderDbContext = Mocker.Create<OrderDbContext>();

            // Given
            Order order = Fixture.Create<Order>();

            // When
            _orderFacade.AddOrder(order);
            await _orderDbContext.SaveChangesAsync();

            // Then
            Assert.AreEqual(1, _orderDbContext.Order.Count());
        }

        [TestMethod]
        public async Task UpdateOrder_OrderIsUpdated()
        {
            // Given

            // Add new order into the database
            _orderFacade.AddOrder(Fixture.Create<Order>());
            await _orderDbContext.SaveChangesAsync();

            // Get the newly added order and update a property
            Order order = await _orderFacade.GetById(_orderDbContext.Order.First().OrderId);
            order.CustomerId = 5;

            // When
            await _orderFacade.UpdateOrder(order);
            await _orderDbContext.SaveChangesAsync();

            // Then
            Assert.AreEqual(5, _orderDbContext.Order.First().CustomerId);
        }

        [TestMethod]
        public async Task DeleteOrder_OrderIsDeleted()
        {
            // Given
            _orderFacade.AddOrder(Fixture.Create<Order>());
            await _orderDbContext.SaveChangesAsync();

            // When
            await _orderFacade.DeleteOrder(_orderDbContext.Order.First().OrderId);
            await _orderDbContext.SaveChangesAsync();

            // Then
            Assert.AreEqual(0, _orderDbContext.Order.Count());
        }
    }
}
