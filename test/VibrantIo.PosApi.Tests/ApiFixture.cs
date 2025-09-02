using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VibrantIo.PosApi.Tests;

public sealed class ApiFixture : IDisposable
{
    private readonly IConfiguration _config = new ConfigurationBuilder().AddUserSecrets<ApiFixture>().Build();
    private readonly ServiceProvider _services;
    private readonly IVibrantPosApiClient _client;
    private readonly IVibrantPosApiClientFactory _factory;

    public string ApiKey => _config["ApiKey"] ?? throw new InvalidOperationException("ApiKey is not configured in user secrets.");
    public bool Sandbox => bool.Parse(_config["Sandbox"] ?? "true");
    public IVibrantPosApiClient Client => _client;
    public IVibrantPosApiClientFactory Factory => _factory;

    public ApiFixture()
    {
        _services = new ServiceCollection()
            .AddVibrantPosApi(options =>
            {
                options.ApiKey = ApiKey;
                options.Sandbox = Sandbox;
            })
            .AddVibrantPosApiFactory()
            .BuildServiceProvider();
        _client = _services.GetRequiredService<IVibrantPosApiClient>();
        _factory = _services.GetRequiredService<IVibrantPosApiClientFactory>();
    }

    public void Dispose() => _services.Dispose();
}
