using Microsoft.Extensions.DependencyInjection;
using VibrantIo.PosApi.Charges;

namespace VibrantIo.PosApi.Tests;

public class ChargesTests
{
    private readonly IVibrantPosApiClient _client;

    public ChargesTests()
    {
        var services = new ServiceCollection()
            .AddVibrantPosApi(options =>
            {
                options.ApiKey = TestSecrets.ApiKey;
                options.Sandbox = TestSecrets.Sandbox;
            })
            .BuildServiceProvider();
        _client = services.GetRequiredService<IVibrantPosApiClient>();
    }

    [Fact]
    public async Task CanListCharges()
    {
        // Given

        // When
        var charges = await _client.Charges.ListChargesAsync().ToListAsync();

        // Then
        Assert.Equal(18, charges.Count);
        Assert.All(charges, x => Assert.Equal(DateTimeKind.Utc, x.Created.Kind));
    }
}
