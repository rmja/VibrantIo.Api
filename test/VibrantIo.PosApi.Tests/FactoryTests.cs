using Microsoft.Extensions.DependencyInjection;

namespace VibrantIo.PosApi.Tests;

public class FactoryTests
{
    [Fact]
    public async Task CanCreateClient()
    {
        // Given
        var services = new ServiceCollection().AddVibrantPosApi().BuildServiceProvider();
        var factory = services.GetRequiredService<IVibrantPosApiClientFactory>();

        // When
        var client = factory.Create(new() { Sandbox = true, ApiKey = TestSecrets.SandboxApiKey });

        // Then
        var terminals = await client.Terminals.GetAllAsync().ToListAsync();
        Assert.Equal(3, terminals.Count);
    }
}
