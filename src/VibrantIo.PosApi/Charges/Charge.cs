namespace VibrantIo.PosApi.Charges;

public record Charge : IPaginateableObject
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    public int Amount { get; set; }
    public string? BalanceTransactionId { get; set; }
    public required string Currency { get; set; }
    public required string PaymentIntent { get; set; }
    public PaymentStatus Status { get; set; }
    public string? FailureCode { get; set; }
    public string? FailureMessage { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}
