using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SharedKernel.ErrorsBase;
using System.Data;

namespace Framework.Database.Transaction;

public class TransactionScope(IDbTransaction transaction, ILogger<ITransactionScope> logger) : ITransactionScope
{
    public UnitResult<Error> Commit()
    {
        try
        {
            transaction.Commit();
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while comitting transaction.");
            return Error.Failure("transaction.commit.failed", "Failed to commit transaction.");
        }
    }

    public UnitResult<Error> Rollback()
    {
        try
        {
            transaction.Rollback();
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while rollbacking transaction.");
            return Error.Failure("transaction.rollback.failed", "Failed to rollback transaction.");
        }
    }

    public void Dispose()
    {
        transaction.Dispose();
    }
}
