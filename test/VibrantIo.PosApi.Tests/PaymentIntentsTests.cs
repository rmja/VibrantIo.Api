using Microsoft.Extensions.DependencyInjection;

namespace VibrantIo.PosApi.Tests;

public class PaymentIntentsTests
{
    private readonly IVibrantPosApiClient _client;

    public PaymentIntentsTests()
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
    public async Task CanGetPaymentIntent()
    {
        // Given
        var id = "pi_3cF5dn8W4qom2CDHvTte8p";

        // When
        var paymentIntent = await _client.PaymentIntents.GetPaymentIntentAsync(
            id,
            TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(id, paymentIntent.Id);
        //Assert.Equal("ti_dtp8BLu1XQusNMWcSQiXnu", paymentIntent.TerminalId);
    }
}
