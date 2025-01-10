using VibrantIo.PosApi.PaymentIntents;

namespace VibrantIo.PosApi.Terminals;

public record ProcessPaymentIntentInit
{
    public required PaymentIntentInit PaymentIntent { get; set; }
}
