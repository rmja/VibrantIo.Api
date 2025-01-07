namespace VibrantIo.PosApi.Models;

public enum PaymentIntentStatus
{
    RequiresPaymentMethod,
    Processing,
    Succeeded,
    Canceled,
}
