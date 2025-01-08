using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace VibrantIo.PosApi;

public class VibrantPosApiClientFactory(
    IServiceProvider services,
    IOptionsSnapshot<VibrantPosApiOptions> optionsSnapshot
) : IVibrantPosApiClientFactory
{
    private readonly ObjectFactory<VibrantPosApiClient> _clientFactory =
        ActivatorUtilities.CreateFactory<VibrantPosApiClient>([typeof(VibrantPosApiOptions)]);

    public IVibrantPosApiClient Create(string name)
    {
        var options = optionsSnapshot.Get(name);
        return Create(options);
    }

    public IVibrantPosApiClient Create(VibrantPosApiOptions options)
    {
        return _clientFactory(services, [options]);
    }
}
