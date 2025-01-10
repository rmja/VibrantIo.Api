using Microsoft.Extensions.DependencyInjection;

namespace VibrantIo.PosApi;

public class VibrantPosApiClientFactory(IServiceProvider services) : IVibrantPosApiClientFactory
{
    private readonly ObjectFactory<VibrantPosApiClient> _clientFactory =
        ActivatorUtilities.CreateFactory<VibrantPosApiClient>([typeof(VibrantPosApiOptions)]);

    public IVibrantPosApiClient Create(VibrantPosApiOptions options)
    {
        return _clientFactory(services, [options]);
    }
}
