namespace Framework.Database;

public interface IMigrator
{
    Task Migrate(CancellationToken cancellationToken = default);
}
