using Microsoft.Extensions.Options;
using VibrantIo.PosApi;

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace Microsoft.Extensions.DependencyInjection;

public static class VibrantPosApiExtensions
{
    public static IServiceCollection AddVibrantPosApi(
        this IServiceCollection services,
        Action<VibrantPosApiOptions>? configureOptions = null
    )
    {
        services.AddHttpClient<VibrantPosApiClient>();

        services.AddSingleton<IVibrantPosApiClientFactory, VibrantPosApiClientFactory>();

        if (configureOptions is not null)
        {
            services
                .AddTransient<IVibrantPosApiClient, VibrantPosApiClient>(provider =>
                {
                    var options = provider.GetRequiredService<IOptions<VibrantPosApiOptions>>();
                    return ActivatorUtilities.CreateInstance<VibrantPosApiClient>(
                        provider,
                        options.Value
                    );
                })
                .Configure(configureOptions);
        }

        return services;
    }
}
