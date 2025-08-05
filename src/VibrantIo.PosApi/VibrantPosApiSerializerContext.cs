using System.Text.Json.Serialization;
using VibrantIo.PosApi.Charges;
using VibrantIo.PosApi.JsonConverters;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Refunds;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

[JsonSerializable(typeof(PaymentIntent))]
[JsonSerializable(typeof(PaymentIntentInit))]
[JsonSerializable(typeof(PagedList<Charge>))]
[JsonSerializable(typeof(PagedList<Refund>))]
[JsonSerializable(typeof(PagedList<Terminal>))]
[JsonSerializable(typeof(Charge))]
[JsonSerializable(typeof(Refund))]
[JsonSerializable(typeof(Terminal))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(ProcessPaymentIntentInit))]
[JsonSerializable(typeof(ProcessRefundInit))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = [
        typeof(UnixTimestampJsonConverter),
        typeof(SnakeCaseLowerJsonStringEnumConverter<PaymentIntentStatus>),
        typeof(SnakeCaseLowerJsonStringEnumConverter<PaymentStatus>),
        typeof(SnakeCaseLowerJsonStringEnumConverter<RefundReason>),
        typeof(SnakeCaseLowerJsonStringEnumConverter<TerminalMode>),
    ]
)]
internal sealed partial class VibrantPosApiSerializerContext : JsonSerializerContext;
