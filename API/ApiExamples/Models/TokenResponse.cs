using System.Text.Json.Serialization;

namespace ApiExamples.Models;

public record TokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; init; }
}