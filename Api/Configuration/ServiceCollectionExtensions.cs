using Api.Behaviors;
using Api.Http.ClientesApi;
using Api.Persistance;
using Asp.Versioning;
using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;
namespace Api.Extensions;

internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures Api Versioning
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options => {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }


    /// <summary>
    /// Register Infrastructure Dependencies, like Databases and Http Clients.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    {
        services.AddDbContext<FeaturesDbContext>(options =>
            options.UseInMemoryDatabase("VerticalSliceDemoDb")
        );
        services.AddRefitClient<IClientesApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api-address.com")
        );

        return services;
    }

    /// <summary>
    /// Registers MediatR, FluentValidations and Carter Endpoints Modules from Assembly.
    /// </summary>
    public static IServiceCollection AddApplicationFeatures(this IServiceCollection services)
    {
        var assemblyToScan = typeof(Program).Assembly;
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assemblyToScan);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assemblyToScan);

        services.AddCarter();

        return services;
    }


    /// <summary>
    /// Configures Swagger and Endpoint Exploration 
    /// (Must Be invoked after Application Features Registration to properly pickup the endpoints)
    /// </summary>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API - V2", Version = "v2" });
            }
         );

        return services;
    }
}
