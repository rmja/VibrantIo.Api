using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using VibrantIo.PosApi;

#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace Microsoft.Extensions.DependencyInjection;

public static class VibrantPosApiExtensions
{
    /// <summary>
    /// Add a default Vibrant.io client to the <paramref name="services"/> service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddVibrantPosApi(
        this IServiceCollection services,
        Action<VibrantPosApiOptions> configureOptions
    )
    {
        AddCoreServices(services);
        services.TryAddTransient<IVibrantPosApiClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<VibrantPosApiOptions>>();
            return ActivatorUtilities.CreateInstance<VibrantPosApiClient>(provider, options.Value);
        });
        services.Configure(configureOptions);
        return services;
    }

    /// <summary>
    /// Add the Vibrant.io client factory to the <paramref name="services"/> service collection.
    /// Named clients can be configured using
    /// <code>services.Configure&lt;VibrantPosApiOptions&gt;("name of client", options => {})</code>
    /// Clients are then obtained from the factory using <see cref="IVibrantPosApiClientFactory"/>.
    /// </summary>
    public static IServiceCollection AddVibrantPosApiFactory(this IServiceCollection services)
    {
        AddCoreServices(services);
        services.TryAddSingleton<IVibrantPosApiClientFactory, VibrantPosApiClientFactory>();
        return services;
    }

    private static void AddCoreServices(IServiceCollection services)
    {
        services.AddHttpClient<VibrantPosApiClient>();
    }
}
