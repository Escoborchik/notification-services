using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace Framework.Database.Transaction;

public interface ITransactionScope : IDisposable
{
    UnitResult<Error> Commit();
    UnitResult<Error> Rollback();
}