using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Api.StartupExtensions
{
	public class AutoFacStartup
	{
		public static IServiceProvider CreateServiceProvider(IServiceCollection services, string dbConnection)
		{
			//setup dependency injection
			var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(new[] { Assembly.Load("Business"), Assembly.Load("Framework") }).AsImplementedInterfaces();

            builder.RegisterType<OrderDbContext>()
                .WithParameter("options", new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer(dbConnection).Options)
                .As<OrderDbContext>().As<IOrderDbContext>().InstancePerLifetimeScope();

            builder.Populate(services);

			// Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(builder.Build());
		}
	}
}
