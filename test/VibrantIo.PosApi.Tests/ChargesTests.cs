using VibrantIo.PosApi.Charges;

namespace VibrantIo.PosApi.Tests;

public class ChargesTests(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    private readonly IVibrantPosApiClient _client = fixture.Client;

    [Fact]
    public async Task CanListCharges()
    {
        // Given

        // When
        var charges = await _client
            .Charges.ListChargesAsync()
            .ToListAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(charges.Count > 18);
        Assert.All(charges, x => Assert.Equal(DateTimeKind.Utc, x.Created.Kind));
    }
}
