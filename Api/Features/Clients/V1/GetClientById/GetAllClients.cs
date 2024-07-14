using Api.Common.Abstractions;
using Api.Domain;
using Api.Features.Clients.V1.CreateClient;
using Api.Persistance;
using Carter;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Api.Middleware.ExceptionHandlingMiddleware;

namespace Api.Features.Clients.V1.GetClientById;

public static class GetClientById
{
    public class Endpoint() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/v{version:apiVersion}/clientes/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new Query(id));
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

    public record Query(long Id) : IQuery<Response>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public record Response(long ClientId, string FirstName, string LastName);

    public class Handler : IQueryHandler<Query, Response>
    {
        private readonly FeaturesDbContext _db;

        public Handler(FeaturesDbContext db)
        {
            _db = db;
        }

        public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _db.Clients.FindAsync(request.Id);
            if (entity is null)
                return Result.Failure<Response>(Errors.NotFound);

            return Result.Success(
                new Response(
                    entity.Id,
                    entity.FirstName,
                    entity.LastName
                )
            );
        }
    }

    public static class Errors
    {

        public static readonly Error NotFound = new(
        "Client.NotFound",
        "The booking with the speficied id was not found");

    }

}
