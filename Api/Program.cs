using Api.Configuration;
using Api.Extensions;
using Api.Middleware;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services
    .AddVersioning()
    .AddInfrastructure()
    .AddApplicationFeatures()
    .AddSwagger();

#endregion

var app = builder.Build();

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
}

app.UseHttpsRedirection();

#endregion

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapVersionedApiEndpoints();
app.Run();
