using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace Notification.Gateway.IntegrationTest;

public sealed class IntegrationFixture : IAsyncLifetime
{
    public PostgreSqlContainer Postgres { get; }

    public IntegrationFixture()
    {
        Postgres = new PostgreSqlBuilder("postgres")
            .WithDatabase("notifications")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await Postgres.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Postgres.StopAsync();
        await Postgres.DisposeAsync();
    }
}
