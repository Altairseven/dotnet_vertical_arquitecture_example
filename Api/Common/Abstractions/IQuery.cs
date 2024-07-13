using MediatR;

namespace Api.Common.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}