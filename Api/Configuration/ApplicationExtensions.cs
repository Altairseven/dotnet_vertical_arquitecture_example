using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;
using Microsoft.OpenApi.Models;

namespace Api.Configuration;

public static class ApplicationExtensions
{
    /// <summary>
    /// Adds Swagger middleware and Maps Open Api Endpoints for Current Api Versions
    /// </summary>
    public static WebApplication UseSwaggerDocs(this WebApplication app) 
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");

        });

        return app;
    }

    /// <summary>
    ///  Creates a VersionSet and Maps it to all endpoints (with the /api prefix)
    /// </summary>
    public static WebApplication MapVersionedApiEndpoints(this WebApplication app) {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
          .HasApiVersion(new ApiVersion(1))
          .HasApiVersion(new ApiVersion(2))
          .ReportApiVersions()
          .Build();

        app.MapGroup("/api")
           .WithApiVersionSet(apiVersionSet)
           .MapCarter();

        return app;
    } 
}
