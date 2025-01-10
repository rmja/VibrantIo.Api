using System.Runtime.CompilerServices;
using Refit;
using VibrantIo.PosApi.Models;

namespace VibrantIo.PosApi;

public interface ITerminals
{
    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_GetTerminals
    [Get("/pos/v1/terminals")]
    internal Task<PagedList<Terminal>> GetAllAsync(
        int limit,
        [AliasAs("starting_after")] string? startingAfter,
        CancellationToken cancellationToken
    );

    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_getTerminal
    [Get("/pos/v1/terminals/{terminalId}")]
    Task<Terminal> GetByIdAsync(string terminalId, CancellationToken cancellationToken = default);

    // https://pos.api.vibrant.app/docs#/terminals/TerminalController_processPaymentIntent
    [Post("/pos/v1/terminals/{terminalId}/process_payment_intent")]
    Task<ProcessPaymentIntent> ProcessPaymentIntentAsync(
        string terminalId,
        ProcessPaymentIntentInit paymentIntent,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default
    );
}

public static class TerminalsOperaionsExtensions
{
    public static async IAsyncEnumerable<Terminal> GetAllAsync(
        this ITerminals terminals,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        const int DefaultLimit = 100;

        var page = await terminals.GetAllAsync(DefaultLimit, null, cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            foreach (var item in page.Data)
            {
                yield return item;
            }

            if (!page.HasMore)
            {
                break;
            }

            page = await terminals.GetAllAsync(
                DefaultLimit,
                page.Data.Last().Id,
                cancellationToken
            );
        }
    }
}
