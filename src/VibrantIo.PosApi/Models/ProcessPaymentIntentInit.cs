namespace VibrantIo.PosApi.Models;

public record ProcessPaymentIntentInit
{
    public required PaymentIntentInit PaymentIntent { get; set; }
}
