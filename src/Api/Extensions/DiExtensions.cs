using ExceptionCatcherMiddleware.Extensions;
using Infrastructure.ExceptionMapping;
using Infrastructure.Mappers.BasketEntity;
using Infrastructure.Repositories;
using Serilog;
using Serilog.Events;
using StackExchange.Redis;
using SwaggerEnrichers.Extensions;

namespace Api.Extensions;

public static class DiExtensions
{
    public static void AddRepository(this IServiceCollection services, BasketRepositoryOptions options)
    {
        services.AddSingleton(options);
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<BasketSerializer>();
    }

    public static void AddMappers(this IServiceCollection services)
    {
        services.AddScoped<DataMapper>();
        services.AddScoped<ViewMapper>();
    }

    public static void AddRedis(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c =>
        {
            var config = ConfigurationOptions.Parse(connectionString, true);
            return ConnectionMultiplexer.Connect(config);
        });
    }

    public static void AddExceptionMappers(this IServiceCollection services)
    {
        services.AddScoped<EntityNotFoundExceptionMapper>();
        services.AddExceptionCatcherMiddlewareServices(builder =>
        {
            builder.RegisterExceptionMapper<BasketNotFoundException, EntityNotFoundExceptionMapper>();
        });
    }
    
    public static void AddSerilog(this WebApplicationBuilder builder, string seqUrl)
    {
        builder.Services.AddHttpContextAccessor();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationIdHeader("x-request-id")
            .WriteTo.Console()
            .WriteTo.Seq(seqUrl)
            .Enrich.WithProperty("ServiceName", "BasketService")
            .CreateLogger();
        builder.Host.UseSerilog();
    }
    
    public static void AddConfiguredSwaggerGen(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.DescribeAllParametersInCamelCase();
            options.AddEnricherFilters();
        });
    }
}