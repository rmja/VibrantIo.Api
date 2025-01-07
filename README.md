# Vibrant.io API Client

.NET Client for the [Vibrant.io](https://vibrant.io) [Push to Pay API](https://pos.api.vibrant.app/docs).

## Nuget Packages

| Package name                      | Description				    | Badge |
|-----------------------------------|-------------------------------|-------|
| `VibrantIo.PosApi`                | The Push to Pay API Client    | [![PosApi](https://img.shields.io/nuget/vpre/VibrantIo.PosApi.svg)](https://www.nuget.org/packages/VibrantIo.PosApi) |

## Usage

Add the client to the DI service container:

```C#
services.AddVibrantPosApi(options => {
    options.Sandbox = false;
    options.ApiKey = "...";
});
```

Then obtain a `IVibrantPosApiClient` to interact with the api.