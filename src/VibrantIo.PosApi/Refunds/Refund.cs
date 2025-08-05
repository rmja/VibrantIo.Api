namespace VibrantIo.PosApi.Refunds;

public record Refund : IPaginateableObject
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    public int Amount { get; set; }
    public required string Currency { get; set; }
    public RefundReason Reason { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}
