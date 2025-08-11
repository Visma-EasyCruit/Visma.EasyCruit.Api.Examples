using System.Net.Http.Json;
using ApiExamples.Models.Language;

namespace ApiExamples;

public class LanguageApi
{
    private const string BaseUrl = "https://easycruit.hrm.int.test.visma.net";

    [Fact]
    public async Task GetCountries()
    {
        using var client = new HttpClient();
        await client.GetFromJsonAsync<List<Country>>($"{BaseUrl}/api/language/v1/countries?locale=en-GB");
    }

    [Fact]
    public async Task GetCounties()
    {
        using var client = new HttpClient();
        await client.GetFromJsonAsync<List<County>>($"{BaseUrl}/api/language/v1/countries/160/counties");
    }

    [Fact]
    public async Task GetMunicipalities()
    {
        using var client = new HttpClient();
        await client.GetFromJsonAsync<List<Municipality>>(
            $"{BaseUrl}/api/language/v1/countries/160/counties/1181/municipalities");
    }

    [Fact]
    public async Task GetTranslations()
    {
        using var client = new HttpClient();
        await client.GetFromJsonAsync<Dictionary<string, string>>($"{BaseUrl}/api/v2/languages/translations/en-GB/portal");
    }
}