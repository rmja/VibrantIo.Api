using VibrantIo.PosApi.Operations;

namespace VibrantIo.PosApi;

public interface IVibrantPosApiClient
{
    IPaymentIntentOperations PaymentIntents { get; }
    ITerminalsOperations Terminals { get; }
}
