using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Refit;
using VibrantIo.PosApi.Operations;

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

    public IPaymentIntentOperations PaymentIntents { get; }

    public ITerminalsOperations Terminals { get; }

    public VibrantPosApiClient(HttpClient httpClient, IOptions<VibrantPosApiOptions> options)
    {
        if (options.Value.Sandbox)
        {
            httpClient.BaseAddress = new("https://pos-api.sandbox.vibrant.app");
        }
        else
        {
            httpClient.BaseAddress = new("https://pos.api.vibrant.app");
        }

        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ApiKey", options.Value.ApiKey);

        PaymentIntents = RestService.For<IPaymentIntentOperations>(httpClient, _refitSettings);
        Terminals = RestService.For<ITerminalsOperations>(httpClient, _refitSettings);
    }
}
