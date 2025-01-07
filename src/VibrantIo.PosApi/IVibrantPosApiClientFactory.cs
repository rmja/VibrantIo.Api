namespace VibrantIo.PosApi;

public interface IVibrantPosApiClientFactory
{
    IVibrantPosApiClient Create(VibrantPosApiOptions options);
}
