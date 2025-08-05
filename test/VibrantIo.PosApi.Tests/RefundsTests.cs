using Microsoft.Extensions.DependencyInjection;
using VibrantIo.PosApi.Refunds;

namespace VibrantIo.PosApi.Tests;

public class RefundsTests
{
    private readonly IVibrantPosApiClient _client;

    public RefundsTests()
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
