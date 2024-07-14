using Carter;
using MediatR;
using Api.Common.Abstractions;

namespace Api.Features.Clients.V2.CreateClient;

public class CreateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/v{version:apiVersion}/clientes", async (CreateClientCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        }).WithTags("Clientes")
        .WithDescription("Crea un cliente nuevo en el Api externo de clientes")
        .Produces<CreateClientResponse>(StatusCodes.Status200OK)
        .Produces<ExceptionDetails>(StatusCodes.Status400BadRequest)
        .Produces<ExceptionDetails>(StatusCodes.Status500InternalServerError)
        .MapToApiVersion(2)
        .WithOpenApi();
    }
}

