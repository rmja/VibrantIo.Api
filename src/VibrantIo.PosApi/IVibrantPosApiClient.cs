using VibrantIo.PosApi.PaymentIntents;

namespace VibrantIo.PosApi;

public interface IVibrantPosApiClient
{
    IPaymentIntents PaymentIntents { get; }
    ITerminals Terminals { get; }
}
