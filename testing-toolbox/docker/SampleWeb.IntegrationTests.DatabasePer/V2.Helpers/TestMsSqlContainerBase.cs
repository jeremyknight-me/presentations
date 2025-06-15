using Testcontainers.MsSql;

namespace SampleWeb.IntegrationTests.DatabasePer.V2.Helpers;

public abstract class TestMsSqlContainerBase : IAsyncLifetime
{
    public MsSqlContainer Container { get; } = new MsSqlBuilder().Build();

    public async ValueTask InitializeAsync()
        => await this.Container.StartAsync();

    public async ValueTask DisposeAsync()
        => await this.Container.DisposeAsync();
}
