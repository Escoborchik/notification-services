using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace Framework.Abstractions;

public interface ICommandHandler<TResult, in TCommand> 
    where TCommand : ICommand
{
    public Task<Result<TResult, Errors>> Execute(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    public Task<UnitResult<Errors>> Execute(TCommand command, CancellationToken cancellationToken);
}
