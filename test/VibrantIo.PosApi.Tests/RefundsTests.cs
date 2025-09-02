using VibrantIo.PosApi.Refunds;

namespace VibrantIo.PosApi.Tests;

public class RefundsTests(ApiFixture fixture) : IClassFixture<ApiFixture>
{
    private readonly IVibrantPosApiClient _client = fixture.Client;

    [Fact]
    public async Task CanListRefunds()
    {
        // Given

        // When
        var refunds = await _client
            .Refunds.ListRefundsAsync()
            .ToListAsync(TestContext.Current.CancellationToken);

        // Then
        Assert.True(refunds.Count > 2);
        Assert.All(refunds, x => Assert.Equal(DateTimeKind.Utc, x.Created.Kind));
    }
}
