namespace VibrantIo.PosApi.Terminals;

public record Terminal
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    public required string Name { get; set; }
    public string? Descriptor { get; set; }
    public TerminalMode Mode { get; set; }
    public bool Virtual { get; set; }
}
