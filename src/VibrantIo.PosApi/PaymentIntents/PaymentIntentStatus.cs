namespace VibrantIo.PosApi.PaymentIntents;

public enum PaymentIntentStatus
{
    RequiresPaymentMethod,
    Processing,
    Succeeded,
    Canceled,
}
