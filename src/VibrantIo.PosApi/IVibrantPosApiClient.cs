using VibrantIo.PosApi.Charges;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Refunds;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

public interface IVibrantPosApiClient
{
    ICharges Charges { get; }
    IPaymentIntents PaymentIntents { get; }
    IRefunds Refunds { get; }
    ITerminals Terminals { get; }
}
