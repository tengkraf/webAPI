using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.StartupExtensions
{
	public static class AutoMapperStartup
	{
		public static IServiceCollection AddAutoMapperStartup(this IServiceCollection services, IServiceProvider serviceProvider)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
                // Collection Mappers allows automapper to determine if an insert, update, or delete needs to occur, by comparing
                // Two collections.  It essentially acts as the Sql Merge statement.
				mc.AddCollectionMappers();

                // UseEntityFrameworkCoreModel automatically sets the field used to determine equality for the collection mappers
                // to the primary key of each table.  It uses the serviceProvider to get the Model from the DBContext.
                mc.UseEntityFrameworkCoreModel<OrderDbContext>(serviceProvider);

				// Adds the mapping configuration below
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			
			services.AddSingleton(mapper);

			return services;
		}
	}

	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            // Reverse Map automatically adds the mapping in the opposite direction
            // AKA CreateMap<DTO.Order, DataAccess.Entities.Order>()
            CreateMap<DataAccess.Entities.Order, DTO.Order>().ReverseMap();
			CreateMap<DataAccess.Entities.OrderedItem, DTO.OrderedItem>().ReverseMap();

            // ForMember allows you to specify custom mapping rules for a particular property
            // In this case the FullAddress DTO property is getting the address fields concatenated together for it
			CreateMap<DataAccess.Entities.ShippingAddress, DTO.ShippingAddress>()
                .ForMember(dest => dest.FullAddress, opt => 
                    opt.MapFrom(src => $"{src.Addr1Text} {src.Addr2Text} {src.CityName} {src.StateCode} {src.CountryCode} {src.ZipCode}"))
                .ReverseMap();
        }
	}
}
