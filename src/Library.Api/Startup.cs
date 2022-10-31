﻿using AutoMapper;
using Library.Database;
using Library.Database.Core;
using Library.Dto;
using Library.Service;
using Library.Service.Interfaces;
using Library.Repository.Core;
using Library.Repository.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Dtos = Library.Dto;
using Entities = Library.Database.Entities;

namespace Library.Startup
{
    public class Startup
    {
        public static void Configure(
            ConfigurationManager configuration,
            IServiceCollection services
        )
        {
            ConfigureInjector(services);
            ConfigureDatabase(configuration, services);
        }

        private static void ConfigureInjector(IServiceCollection services)
        {
            IMapper mapper = MappingProfile.GetMapper();

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEventStoreService, EventStoreService>()
                .AddSingleton(mapper);
        }

        private static void ConfigureDatabase(
            ConfigurationManager configuration,
            IServiceCollection services
        )
        {
            string connectionString = configuration["Database:ConnectionString"];

            services.AddDbContext<LibraryContext>(options => options.UseSqlite(connectionString));
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity-To-DTO

            CreateMap<Entities.EventStore, Dtos.EventStore>()
                .ForMember(
                    dest => dest.Data,
                    opt =>
                        opt.MapFrom(
                            src =>
                                JsonSerializer.Deserialize<object>(
                                    src.Data,
                                    (JsonSerializerOptions)null
                                )
                        )
                )
                .ForAllMembers((opts) => opts.Condition((src, dest, member) => member != null));

            // DTO-To-Entity

            CreateMap<Dtos.EventStore, Entities.EventStore>()
                .ForMember(
                    dest => dest.Data,
                    opt =>
                        opt.MapFrom(
                            src => JsonSerializer.Serialize(src.Data, (JsonSerializerOptions)null)
                        )
                )
                .IgnoreDtoAuditMembers();
        }

        public static IMapper GetMapper()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }

    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreDtoAuditMembers<
            TSource,
            TDestination
        >(this IMappingExpression<TSource, TDestination> expression)
            where TSource : DtoBase
            where TDestination : EntityBase
        {
            return expression
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedAt, opts => opts.Ignore());
        }
    }
}