using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Refunds;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi.Tests;

public class TerminalsTests
{
    private readonly IVibrantPosApiClient _client;
    private const string TerminalId = "ti_dtp8BLu1XQusNMWcSQiXnu"; // TJ2

    public TerminalsTests()
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
    public async Task CanListTerminals()
    {
        // Given

        // When
        var terminals = await _client.Terminals.ListTerminalsAsync().ToListAsync();

        // Then
        Assert.Equal(3, terminals.Count);
        Assert.All(terminals, x => Assert.Equal(TerminalMode.Terminal, x.Mode));
    }

    [Fact]
    public async Task CanGetTerminal()
    {
        // Given

        // When
        var terminal = await _client.Terminals.GetTerminalAsync(TerminalId);

        // Then
        Assert.Equal("TJ2", terminal.Name);
    }

    [Fact]
    public async Task CanProcessPaymentIntent()
    {
        // Given
        var idempotencyKey = Guid.NewGuid().ToString();

        // When
        var response = await _client.Terminals.ProcessPaymentIntentAsync(
            TerminalId,
            new()
            {
                PaymentIntent = new() { Amount = 1234, Description = "Testkøb" },
            },
            idempotencyKey
        );

        var exception = await Assert.ThrowsAsync<VibrantApiException>(() =>
            _client.Terminals.ProcessPaymentIntentAsync(
                TerminalId,
                new()
                {
                    PaymentIntent = new()
                    {
                        Amount = 1234,
                        Description = "Should fail because the same idempotency key is used",
                    },
                },
                idempotencyKey
            )
        );

        // Then
        Assert.Equal(TerminalId, response.TerminalId);
        Assert.NotNull(response.ObjectIdToProcess);

        var paymentIntent = await _client.PaymentIntents.GetPaymentIntentAsync(
            response.ObjectIdToProcess
        );
        Assert.Equal(1234, paymentIntent.Amount);
        Assert.Equal(PaymentIntentStatus.RequiresPaymentMethod, paymentIntent.Status);
        Assert.Empty(paymentIntent.CancelationReason);

        Assert.Equal(500, exception.Status);
    }

    [Fact]
    public async Task CanProcessRefund()
    {
        // Given
        const string chargeId = "ch_ZQwCWdDzUq8rZ5VCPrSiSY";

        // When
        var response = await _client.Terminals.ProcessRefundAsync(
            TerminalId,
            new()
            {
                Refund = new() { ChargeId = chargeId, Description = "Test refund" },
            },
            idempotencyKey: null,
            TestContext.Current.CancellationToken
        );

        // Then
        Assert.Equal(TerminalId, response.TerminalId);
        Assert.NotNull(response.ObjectIdToProcess);

        var refund = await _client.Refunds.GetRefundAsync(response.ObjectIdToProcess);
        Assert.Equal(1234, refund.Amount);
        Assert.Equal(RefundReason.RequestedByCustomer, refund.Reason);
        Assert.Equal("DKK", refund.Currency);
    }
}
