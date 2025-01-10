using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

public interface IVibrantPosApiClient
{
    IPaymentIntents PaymentIntents { get; }
    ITerminals Terminals { get; }
}
