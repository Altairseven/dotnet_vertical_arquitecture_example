using Refit;

namespace Api.Http.ClientesApi;

public interface IClientesApi
{
    [Post("/api/clientes")]
    Task<Guid> PostCliente(PostClienteRequest request);
}

public record PostClienteRequest(string Nombre, string Apellido);