using Refit;

namespace VibrantIo.PosApi.Charges;

public interface ICharges
{
    [Get("/pos/v1/charges")]
    internal Task<PagedList<Charge>> ListChargesAsync(
        int limit,
        [AliasAs("starting_after")] string? startingAfter,
        CancellationToken cancellationToken = default
    );

    [Get("/pos/v1/charges/{chargeId}")]
    Task<Charge> GetChargeAsync(string chargeId, CancellationToken cancellationToken = default);
}

public static class ChargesExtensions
{
    public static IAsyncEnumerable<Charge> ListChargesAsync(this ICharges charges) =>
        new PaginationEnumerable<Charge>(
            (lastId, cancellationToken) =>
                charges.ListChargesAsync(
                    Constants.DefaultPaginationLimit,
                    startingAfter: lastId,
                    cancellationToken
                )
        );
}
