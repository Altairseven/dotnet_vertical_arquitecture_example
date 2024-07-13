using Api.Common.Abstractions;
using Api.Domain;
using Api.Persistance;
using MediatR;

namespace Api.Features.Clients.V1.CreateClient;

public class AddClientHandler : ICommandHandler<AddClientCommand, AddClientResponse>
{
    private FeaturesDbContext _featuresDbContext;

    public AddClientHandler(FeaturesDbContext featuresDbContext)
    {
        _featuresDbContext = featuresDbContext;
    }

    public async Task<Result<AddClientResponse>> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        var cli = Client.NewClient(request.FirstName, request.LastName);
        _featuresDbContext.Clients.Add(cli);

        await _featuresDbContext.SaveChangesAsync();

        return new Result<AddClientResponse>(
            new AddClientResponse(
                cli.Id,
                cli.FirstName,
                cli.LastName),
            true,
            Error.None
        );
    }
}
