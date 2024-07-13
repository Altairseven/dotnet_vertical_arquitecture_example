using MediatR;

namespace Api.Common.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
 where TQuery : IQuery<TResponse>
{

}
