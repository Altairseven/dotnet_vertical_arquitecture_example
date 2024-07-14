using Api.Common.Abstractions;
using Api.Domain;
using Api.Features.Clients.V1.CreateClient;
using Api.Persistance;
using Carter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Api.Middleware.ExceptionHandlingMiddleware;

namespace Api.Features.Clients.V1.GetAllClients;

public static class GetAllClients
{
    public record Query() : IQuery<Response>;

    public record Response(IEnumerable<ClientDto> Clients);

    public record ClientDto(long ClientId, string FirstName, string LastName);

    public class Handler : IQueryHandler<Query, Response>
    {
        private readonly FeaturesDbContext _db;

        public Handler(FeaturesDbContext db)
        {
            _db = db;
        }

        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var clients = await _db.Clients.ToListAsync();

            var mapped = clients.Select(x => 
                new ClientDto(
                    x.Id,
                    x.FirstName, 
                    x.LastName
            )).ToList();

            return Result.Success(new Response(mapped));
        }
    }

    public class Endpoint() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v{version:apiVersion}/clientes", async (ISender sender) =>
            {
                var result = await sender.Send(new Query());
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);

            }).WithTags("Clientes")
            .WithDescription("Trae todos los clientes")
            .Produces<Response>(StatusCodes.Status200OK)
            .Produces<ExceptionDetails>(StatusCodes.Status400BadRequest)
            .Produces<ExceptionDetails>(StatusCodes.Status500InternalServerError)
            .MapToApiVersion(1)
            .WithOpenApi();
        }
    }
}
