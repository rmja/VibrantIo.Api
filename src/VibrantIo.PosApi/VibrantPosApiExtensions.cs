using VibrantIo.PosApi;

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace Microsoft.Extensions.DependencyInjection;

public static class VibrantPosApiExtensions
{
    public static IServiceCollection AddVibrantPosApi(
        this IServiceCollection services,
        Action<VibrantPosApiOptions>? configureAction = null
    )
    {
        services.AddHttpClient<VibrantPosApiClient>();

        services.AddSingleton<IVibrantPosApiClientFactory, VibrantPosApiClientFactory>();

        if (configureAction is not null)
        {
            services.AddTransient<IVibrantPosApiClient, VibrantPosApiClient>().Configure(configureAction);
        }

        return services;
    }
}
