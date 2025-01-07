namespace VibrantIo.PosApi.Models;

public record PaymentIntentInit
{
    public required int Amount { get; set; }
    public required string Description { get; set; }
    public string? Currency { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}
