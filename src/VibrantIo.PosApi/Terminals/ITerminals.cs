using System.Runtime.CompilerServices;
using Refit;

namespace VibrantIo.PosApi.Terminals;

public interface ITerminals
{
    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_GetTerminals
    [Get("/pos/v1/terminals")]
    internal Task<PagedList<Terminal>> ListTerminalsAsync(
        int limit,
        [AliasAs("starting_after")] string? startingAfter,
        CancellationToken cancellationToken
    );

    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_getTerminal
    [Get("/pos/v1/terminals/{terminalId}")]
    Task<Terminal> GetTerminalAsync(
        string terminalId,
        CancellationToken cancellationToken = default
    );

    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_processPaymentIntent
    [Post("/pos/v1/terminals/{terminalId}/process_payment_intent")]
    Task<CommandResponse> ProcessPaymentIntentAsync(
        string terminalId,
        ProcessPaymentIntentInit paymentIntent,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default
    );

    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_processRefund
    [Post("/pos/v1/terminals/{terminalId}/process_refund")]
    Task<CommandResponse> ProcessRefundAsync(
        string terminalId,
        ProcessRefundInit refund,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default
    );
}

public static class TerminalsExtensions
{
    public static IAsyncEnumerable<Terminal> ListTerminalsAsync(this ITerminals terminals) =>
        new PaginationEnumerable<Terminal>(
            (lastId, cancellationToken) =>
                terminals.ListTerminalsAsync(
                    Constants.DefaultPaginationLimit,
                    startingAfter: lastId,
                    cancellationToken
                )
        );
}
