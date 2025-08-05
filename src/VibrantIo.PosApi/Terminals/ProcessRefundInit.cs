using VibrantIo.PosApi.Refunds;

namespace VibrantIo.PosApi.Terminals;

public record ProcessRefundInit
{
    public required RefundInit Refund { get; set; }
}
