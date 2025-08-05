namespace VibrantIo.PosApi;

internal class ErrorResponse
{
    public int Status
    {
        get => StatusCode;
        set => StatusCode = value;
    }
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public string[] Message { get; set; } = [];
}
