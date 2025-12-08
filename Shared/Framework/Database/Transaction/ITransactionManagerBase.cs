using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace Framework.Database.Transaction;

public interface ITransactionManagerBase
{
    Task<Result<ITransactionScope, Error>> BeginTransaction(CancellationToken ct = default);
    Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken = default);
}