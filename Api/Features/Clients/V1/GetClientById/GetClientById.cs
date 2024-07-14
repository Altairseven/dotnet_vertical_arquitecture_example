using Api.Common.Abstractions;
using Api.Persistance;
using Carter;
using FluentValidation;
using MediatR;

namespace Api.Features.Clients.V1.GetClientById;

public class Endpoint() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/v{version:apiVersion}/clientes/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetClientById.Query(id));
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);

        }).WithTags("Clientes")
        .WithDescription("Trae todos los clientes")
        .Produces<GetClientById.GetClientByIdResponse>(StatusCodes.Status200OK)
        .Produces<ExceptionDetails>(StatusCodes.Status400BadRequest)
        .Produces<ExceptionDetails>(StatusCodes.Status500InternalServerError)
        .MapToApiVersion(1)
        .WithOpenApi();
    }
}

public static class GetClientById
{
    public record Query(int Id) : IQuery<GetClientByIdResponse>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public record GetClientByIdResponse(long ClientId, string FirstName, string LastName);

    public class Handler : IQueryHandler<Query, GetClientByIdResponse>
    {
        private readonly FeaturesDbContext _db;

        public Handler(FeaturesDbContext db)
        {
            _db = db;
        }

        public async Task<Result<GetClientByIdResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _db.Clients.FindAsync(request.Id);
            if (entity is null)
                return Result.Failure<GetClientByIdResponse>(Errors.NotFound);

            return Result.Success(
                new GetClientByIdResponse(
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
