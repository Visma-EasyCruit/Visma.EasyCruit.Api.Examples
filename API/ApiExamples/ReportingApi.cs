using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using ApiExamples.Models;

namespace ApiExamples;

public class ReportingApi
{
    private const string BaseUrl = "https://easycruit.hrm.int.test.visma.net";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultBufferSize = 1280, AllowTrailingCommas = true, PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task GetXmlReport()
    {
        using var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{BaseUrl}/api/reporting/v1/data-extract/tenants/391/applications" +
                                 $"?fromDate=2025-01-13&toDate=2025-05-13&departments=4693&departments=74333&locale=en-US"),
            Method = HttpMethod.Get,
            Headers =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                },
                Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(client))
            }
        };

        await client.SendAsync(request);
    }

    [Fact]
    public async Task GetJsonReport()
    {
        using var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{BaseUrl}/api/reporting/v1/data-extract/tenants/391/applications" +
                                 $"?fromDate=2025-01-13&toDate=2025-05-13&departments=4693&departments=74333&locale=en-US"),
            Method = HttpMethod.Get,
            Headers =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json)
                },
                Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(client))
            }
        };

        await client.SendAsync(request);
    }

    [Fact]
    public async Task GetJsonStreamReport()
    {
        using var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{BaseUrl}/api/reporting/v1/data-extract/tenants/391/applications" +
                                 $"?fromDate=2022-01-13&toDate=2022-05-13&departments=4693&departments=74333&locale=en-US"),
            Method = HttpMethod.Get,
            Headers =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue("application/stream+json")
                },
                Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(client))
            }
        };
        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        await foreach (var reportData in JsonSerializer.DeserializeAsyncEnumerable<ApplicationReport>(responseStream,
                           _jsonOptions))
        {
            Console.WriteLine("Candidate ID: " + reportData.CandidateId);
        }
    }

    private static async Task<string> GetTokenAsync(HttpClient client)
    {
        // https://connect.visma.com/connect/token for production
        const string tokenUrl = "https://connect.identity.stagaws.visma.com/connect/token"; // for int.test and stage
        var keys = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
            new("client_id", "your-client-id"),
            new("client_secret", "your-client-secret"),
            new("scope", "easycruit-public-api-int-test:reporting:read-application-report"),
            new("tenant_id", "your-tenant-id")
        };

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(tokenUrl),
            Content = new FormUrlEncodedContent(keys)
        };

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return content?.AccessToken ?? throw new Exception("Token not found.");
    }
}