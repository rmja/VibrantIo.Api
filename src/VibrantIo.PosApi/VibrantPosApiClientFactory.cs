using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace VibrantIo.PosApi;

public class VibrantPosApiClientFactory(IServiceProvider services) : IVibrantPosApiClientFactory
{
    private readonly ObjectFactory<VibrantPosApiClient> _clientFactory =
        ActivatorUtilities.CreateFactory<VibrantPosApiClient>([typeof(IOptions<VibrantPosApiOptions>)]);

    public IVibrantPosApiClient Create(VibrantPosApiOptions options)
    {
        var apiOptions = Options.Create(options);
        var client = _clientFactory(services, [apiOptions]);
        return client;
    }
}
