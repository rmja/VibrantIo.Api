﻿using Refit;

namespace VibrantIo.PosApi.PaymentIntents;

public interface IPaymentIntents
{
    // https://pos.api.vibrant.app/docs#/payment_intents/PaymentIntentController_getPaymentIntent
    [Get("/pos/v1/payment_intents/{paymentIntentId}")]
    Task<PaymentIntent> GetPaymentIntentAsync(
        string paymentIntentId,
        CancellationToken cancellationToken = default
    );
}
