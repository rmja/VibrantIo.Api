using Microsoft.Extensions.DependencyInjection;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi.Tests;

public class TerminalsTests
{
    private readonly IVibrantPosApiClient _client;
    private const string TerminalId = "30320391"; // Terminal 1 in VB sandbox

    public TerminalsTests()
    {
        var services = new ServiceCollection()
            .AddVibrantPosApi(options =>
            {
                options.Sandbox = true;
                options.ApiKey = TestSecrets.SandboxApiKey;
            })
            .BuildServiceProvider();
        _client = services.GetRequiredService<IVibrantPosApiClient>();
    }

    [Fact]
    public async Task CanGetAll()
    {
        // Given

        // When
        var terminals = await _client.Terminals.GetAllAsync().ToListAsync();

        // Then
        Assert.Equal(3, terminals.Count);
        Assert.All(terminals, x => Assert.Equal(TerminalMode.Terminal, x.Mode));
    }

    [Fact]
    public async Task CanGetById()
    {
        // Given

        // When
        var terminal = await _client.Terminals.GetByIdAsync(TerminalId);

        // Then
        Assert.Equal("Terminal 1", terminal.Name);
    }

    [Fact]
    public async Task CanProcessPaymentIntent()
    {
        // Given
        var idempotencyKey = Guid.NewGuid().ToString();

        // When
        var ppi = await _client.Terminals.ProcessPaymentIntentAsync(
            TerminalId,
            new()
            {
                PaymentIntent = new() { Amount = 1234, Description = "Testkøb" }
            },
            idempotencyKey
        );

        var exception = await Assert.ThrowsAsync<VibrantApiException>(
            () =>
                _client.Terminals.ProcessPaymentIntentAsync(
                    TerminalId,
                    new()
                    {
                        PaymentIntent = new()
                        {
                            Amount = 1234,
                            Description = "Should fail because the same idempotency key is used"
                        }
                    },
                    idempotencyKey
                )
        );

        // Then
        Assert.Equal(TerminalId, ppi.TerminalId);
        Assert.NotNull(ppi.ObjectIdToProcess);

        var pi = await _client.PaymentIntents.GetByIdAsync(ppi.ObjectIdToProcess);
        Assert.Equal(1234, pi.Amount);
        Assert.Equal(PaymentIntentStatus.RequiresPaymentMethod, pi.Status);
        Assert.Empty(pi.CancelationReason);

        Assert.Equal(500, exception.Status);
    }
}
