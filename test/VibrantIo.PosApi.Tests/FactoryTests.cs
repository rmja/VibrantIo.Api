using Microsoft.Extensions.DependencyInjection;

namespace VibrantIo.PosApi.Tests;

public class FactoryTests
{
    [Fact]
    public async Task CanCreateClientFromName()
    {
        // Given
        var services = new ServiceCollection()
            .AddVibrantPosApiFactory()
            .Configure<VibrantPosApiOptions>(
                "my-test-client",
                options =>
                {
                    options.Sandbox = true;
                    options.ApiKey = TestSecrets.SandboxApiKey;
                }
            )
            .BuildServiceProvider(validateScopes: true);
        var factory = services.GetRequiredService<IVibrantPosApiClientFactory>();

        // When
        var client = factory.Create("my-test-client");

        // Then
        var terminals = await client.Terminals.GetAllAsync().ToListAsync();
        Assert.Equal(3, terminals.Count);
    }

    [Fact]
    public async Task CanCreateClientFromOptions()
    {
        // Given
        var services = new ServiceCollection()
            .AddVibrantPosApiFactory()
            .BuildServiceProvider(validateScopes: true);
        var factory = services.GetRequiredService<IVibrantPosApiClientFactory>();

        // When
        var client = factory.Create(
            new VibrantPosApiOptions() { Sandbox = true, ApiKey = TestSecrets.SandboxApiKey }
        );

        // Then
        var terminals = await client.Terminals.GetAllAsync().ToListAsync();
        Assert.Equal(3, terminals.Count);
    }
}
