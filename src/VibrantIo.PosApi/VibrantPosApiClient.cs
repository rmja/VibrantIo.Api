using System.Net.Http.Json;
using Refit;
using VibrantIo.PosApi.Charges;
using VibrantIo.PosApi.PaymentIntents;
using VibrantIo.PosApi.Terminals;

namespace VibrantIo.PosApi;

public class VibrantPosApiClient : IVibrantPosApiClient
{
    private static readonly RefitSettings _refitSettings =
        new()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                VibrantPosApiSerializerContext.Default.Options
            ),
            ExceptionFactory = async response =>
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                var error = await response.Content.ReadFromJsonAsync(
                    VibrantPosApiSerializerContext.Default.ErrorResponse
                );
                return new VibrantApiException(error!.Status, error.Error);
            }
        };

    public ICharges Charges { get; }

    public IPaymentIntents PaymentIntents { get; }

    public ITerminals Terminals { get; }

    public VibrantPosApiClient(HttpClient httpClient, VibrantPosApiOptions options)
    {
        httpClient.BaseAddress ??= options.Sandbox
            ? new("https://pos-api.sandbox.vibrant.app")
            : new("https://pos.api.vibrant.app");

        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ApiKey", options.ApiKey);

        Charges = RestService.For<ICharges>(httpClient, _refitSettings);
        PaymentIntents = RestService.For<IPaymentIntents>(httpClient, _refitSettings);
        Terminals = RestService.For<ITerminals>(httpClient, _refitSettings);
    }
}
