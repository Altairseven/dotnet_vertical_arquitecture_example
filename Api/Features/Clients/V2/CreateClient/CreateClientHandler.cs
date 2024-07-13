using Api.Common.Abstractions;
using Api.Domain;
using Api.Http.ClientesApi;
using Api.Persistance;
using MediatR;

namespace Api.Features.Clients.V2.CreateClient;

public class CreateClientHandler : ICommandHandler<CreateClientCommand, CreateClientResponse>
{
    private IClientesApi _clientApi;

    public CreateClientHandler(IClientesApi clientApi)
    {
        _clientApi = clientApi;
    }

    public async Task<Result<CreateClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var guidId = await _clientApi.PostCliente(
            new PostClienteRequest(request.FirstName, request.LastName)
        );

        return new Result<CreateClientResponse>(
            new CreateClientResponse(
                guidId,
                request.FirstName,
                request.LastName),
            true,
            Error.None
        );
    }
}
