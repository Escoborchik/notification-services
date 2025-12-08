using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SharedKernel.ErrorsBase;

namespace Framework.Database.Transaction;

public abstract class TransactionManagerBase<TContext>(TContext dbContext, ILogger logger, ILoggerFactory loggerFactory)
    : ITransactionManagerBase where TContext : DbContext
{
    public virtual async Task<Result<ITransactionScope, Error>> BeginTransaction(CancellationToken ct = default)
    {
        try
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(ct);
            var scopeLogger = loggerFactory.CreateLogger<ITransactionScope>();
            var transactionScope = new TransactionScope(transaction.GetDbTransaction(), scopeLogger);
            return transactionScope;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while beginning a transaction.");
            return Error.Failure("transaction.begin.failed", "Failed to begin transaction.");
        }

    }

    public virtual async Task<UnitResult<Error>> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await dbContext.SaveChangesAsync(ct);
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while saving changes");
            return Error.Failure("database.save.failed", "Failed to save changes to the database.");
        }
    }
}