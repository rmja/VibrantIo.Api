using System.Text.Json;

namespace VibrantIo.PosApi.Tests;

public class ErrorTests
{
    [Fact]
    public void CanDeserializeErrorResponse_WithSingleError()
    {
        // Given
        var json = """
            {
                "status": 400,
                "error": "invalid_request_error",
                "message": "The provided terminal ID is invalid."
            }
            """;
        // When
        var errorResponse = JsonSerializer.Deserialize(
            json,
            VibrantPosApiSerializerContext.Default.ErrorResponse
        );
        // Then
        Assert.NotNull(errorResponse);
        Assert.Equal(400, errorResponse.Status);
        Assert.Equal("invalid_request_error", errorResponse.Error);
        Assert.Single(errorResponse.Message);
        Assert.Equal("The provided terminal ID is invalid.", errorResponse.Message[0]);
    }

    [Fact]
    public void CanDeserializeErrorResponse_WithMultipleErrors()
    {
        // Given
        var json = """
            {
                "status": 400,
                "error": "invalid_request_error",
                "message": [
                    "The provided terminal ID is invalid."
                ]
            }
            """;
        // When
        var errorResponse = JsonSerializer.Deserialize(
            json,
            VibrantPosApiSerializerContext.Default.ErrorResponse
        );
        // Then
        Assert.NotNull(errorResponse);
        Assert.Equal(400, errorResponse.Status);
        Assert.Equal("invalid_request_error", errorResponse.Error);
        Assert.Single(errorResponse.Message);
        Assert.Equal("The provided terminal ID is invalid.", errorResponse.Message[0]);
    }
}
