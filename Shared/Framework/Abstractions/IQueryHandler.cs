using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace Framework.Abstractions;

public interface IQueryHandler<TResponse, in TQuery> 
    where TQuery : IQuery
{
    public Task<TResponse> Execute(TQuery query, CancellationToken ct);
}

public interface IQueryHandlerWithResult<TResponse, in TQuery>
    where TQuery : IQuery
{
    public Task<Result<TResponse, Errors>> Execute(TQuery query, CancellationToken ct = default);
}
