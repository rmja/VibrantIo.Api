namespace VibrantIo.PosApi.Terminals;

public record CommandResponse
{
    public required string TerminalId { get; set; }
    public required string ObjectIdToProcess { get; set; }
    public required string CommandToProcess { get; set; }
}
