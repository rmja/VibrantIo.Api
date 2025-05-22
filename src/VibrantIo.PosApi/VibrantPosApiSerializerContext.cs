using System.Text.Json.Serialization;
using VibrantIo.PosApi.JsonConverters;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

[JsonSerializable(typeof(PaymentIntent))]
[JsonSerializable(typeof(PaymentIntentInit))]
[JsonSerializable(typeof(PagedList<Terminal>))]
[JsonSerializable(typeof(Terminal))]
[JsonSerializable(typeof(ProcessPaymentIntent))]
[JsonSerializable(typeof(ProcessPaymentIntentInit))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters =
    [
        typeof(SnakeCaseLowerJsonStringEnumConverter<PaymentIntentStatus>),
        typeof(SnakeCaseLowerJsonStringEnumConverter<TerminalMode>)
    ]
)]
internal sealed partial class VibrantPosApiSerializerContext : JsonSerializerContext;
