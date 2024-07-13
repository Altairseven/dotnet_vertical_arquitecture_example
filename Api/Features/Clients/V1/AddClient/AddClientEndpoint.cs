using Asp.Versioning.Builder;
using Asp.Versioning;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Api.Middleware.ExceptionHandlingMiddleware;

namespace Api.Features.Clients.V1.CreateClient;

public class AddClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/v{version:apiVersion}/clientes", async (AddClientCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithTags("Clientes")
        .WithDescription("Crea un cliente nuevo")
        .Produces<AddClientResponse>(StatusCodes.Status200OK)
        .Produces<ExceptionDetails>(StatusCodes.Status400BadRequest)
        .Produces<ExceptionDetails>(StatusCodes.Status500InternalServerError)
        .MapToApiVersion(1)
        .WithOpenApi();
    }
}

