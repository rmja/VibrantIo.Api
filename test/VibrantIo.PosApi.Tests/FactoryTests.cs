using Microsoft.Extensions.DependencyInjection;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi.Tests;

public class FactoryTests
{
    [Fact]
    public async Task CanCreateClient()
    {
        // Given
        var services = new ServiceCollection()
            .AddVibrantPosApiFactory()
            .BuildServiceProvider(validateScopes: true);
        var factory = services.GetRequiredService<IVibrantPosApiClientFactory>();

        // When
        var client = factory.Create(
            new() { ApiKey = TestSecrets.ApiKey, Sandbox = TestSecrets.Sandbox }
        );

        // Then
        var terminals = await client.Terminals.GetAllAsync().ToListAsync();
        Assert.Equal(2, terminals.Count);
    }
}
