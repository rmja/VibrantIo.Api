using Refit;

namespace VibrantIo.PosApi.Refunds;

public interface IRefunds
{
    [Get("/pos/v1/refunds")]
    internal Task<PagedList<Refund>> ListRefundsAsync(
        int limit,
        [AliasAs("starting_after")] string? startingAfter,
        CancellationToken cancellationToken = default
    );

    [Get("/pos/v1/refunds/{refundId}")]
    Task<Refund> GetRefundAsync(string refundId, CancellationToken cancellationToken = default);
}

public static class RefundsExtensions
{
    public static IAsyncEnumerable<Refund> ListRefundsAsync(this IRefunds refunds) =>
        new PaginationEnumerable<Refund>(
            (lastId, cancellationToken) =>
                refunds.ListRefundsAsync(
                    Constants.DefaultPaginationLimit,
                    startingAfter: lastId,
                    cancellationToken
                )
        );
}
