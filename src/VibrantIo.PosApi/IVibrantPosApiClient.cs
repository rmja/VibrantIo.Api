using VibrantIo.PosApi.Charges;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

public interface IVibrantPosApiClient
{
    ICharges Charges { get; }
    IPaymentIntents PaymentIntents { get; }
    ITerminals Terminals { get; }
}
