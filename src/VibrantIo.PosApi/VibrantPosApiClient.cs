using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

public class VibrantPosApiClient : IVibrantPosApiClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
        };
    private static readonly RefitSettings _refitSettings =
        new()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(_jsonSerializerOptions),
            ExceptionFactory = async response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return new VibrantApiException(error!.Status, error.Error);
            }
        };

    public IPaymentIntents PaymentIntents { get; }

    public ITerminals Terminals { get; }

    public VibrantPosApiClient(HttpClient httpClient, VibrantPosApiOptions options)
    {
        if (options.Sandbox)
        {
            httpClient.BaseAddress = new("https://pos-api.sandbox.vibrant.app");
        }
        else
        {
            httpClient.BaseAddress = new("https://pos.api.vibrant.app");
        }

        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ApiKey", options.ApiKey);

        PaymentIntents = RestService.For<IPaymentIntents>(httpClient, _refitSettings);
        Terminals = RestService.For<ITerminals>(httpClient, _refitSettings);
    }
}
