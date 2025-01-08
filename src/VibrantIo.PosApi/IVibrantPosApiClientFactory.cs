namespace VibrantIo.PosApi;

public interface IVibrantPosApiClientFactory
{
    IVibrantPosApiClient Create(string name);
    IVibrantPosApiClient Create(VibrantPosApiOptions options);
}
