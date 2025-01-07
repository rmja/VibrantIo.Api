namespace VibrantIo;

public class VibrantApiException(int status, string? message) : Exception(message)
{
    public int Status { get; } = status;
}
