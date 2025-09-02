using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi.Tests;

public class FactoryTests(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    [Fact]
    public async Task CanCreateClient()
    {
        // Given

        // When
        var client = fixture.Factory.Create(new VibrantPosApiOptions
        {
            ApiKey = fixture.ApiKey,
            Sandbox = fixture.Sandbox,
        });

        // Then
        var terminals = await client
            .Terminals.ListTerminalsAsync()
            .ToListAsync(TestContext.Current.CancellationToken);
        Assert.Equal(3, terminals.Count);
    }
}
