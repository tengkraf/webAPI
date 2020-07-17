using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Business;
using DataAccess;
using Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.StartupExtensions;

namespace IntegrationTests.Helpers
{
    public class BaseIntegrationTest
    {
        protected AutoMock Mocker { get; set; }
        protected Fixture Fixture { get; set; }

        [TestCleanup]
        public void TestCleanup()
        {
            Mocker.Dispose();
        }

        protected void CreateMockerWithInMemoryDB()
        {
            Mocker = AutoMock.GetLoose(builder =>
            {
                builder.RegisterAssemblyTypes(Assembly.Load("Business")).AsImplementedInterfaces();

                builder.RegisterType<AppConfig>().As<IAppConfig>().InstancePerLifetimeScope();
                builder.RegisterType<LoggingService>().As<ILoggingService>().SingleInstance();
                builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).InstancePerLifetimeScope();

                // Register in memory database for DBContext
                builder.RegisterType<OrderDbContext>()
                    .WithParameter("options", new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options)
                    .As<OrderDbContext>().As<IOrderDbContext>().InstancePerLifetimeScope();

                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddCollectionMappers();
                    mc.UseEntityFrameworkCoreModel<OrderDbContext>(
                        new OrderDbContext(new DbContextOptionsBuilder<OrderDbContext>().UseInMemoryDatabase("mapperModel").Options).Model);

                    // Configuration code
                    mc.AddProfile(new MappingProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();

                builder.RegisterInstance<IMapper>(mapper);
            });
        }

        protected void SetupAutoFixtureWhichIgnoresIdFields()
        {
            Fixture = new Fixture();
            Fixture.Customizations.Add(new NoIdSpecimenBuilder());
        }
    }
}
