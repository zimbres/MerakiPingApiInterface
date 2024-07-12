namespace MerakiPingApiInterface.Services;

public class RequestPingService
{
    private readonly ILogger<RequestPingService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RequestPingService(ILogger<RequestPingService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        var proxyConfiguration = _configuration.GetSection(nameof(ProxyConfiguration)).Get<ProxyConfiguration>();
        _httpClient = _httpClientFactory.CreateClient(proxyConfiguration!.UseProxy ? "Proxy" : "Default");
    }

    public async Task<ReturnRequestPing> Execute(string device, string target)
    {
        var merakiApiConfiguration = _configuration.GetSection(nameof(MerakiApiConfiguration)).Get<MerakiApiConfiguration>()!;

        var payload = new RequestPingPayload
        {
            Target = target,
            Count = merakiApiConfiguration.Count
        };

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {merakiApiConfiguration.Token}");

        try
        {
            var returnPingResult = await _httpClient.PostAsJsonAsync($"{merakiApiConfiguration.BaseAddress}/{device}/liveTools/ping", payload);
            var content = await returnPingResult.Content.ReadAsStringAsync();

            if (content.Contains("errors"))
            {
                var errors = JsonDocument.Parse(content).RootElement.GetProperty("errors");
                foreach (JsonElement error in errors.EnumerateArray())
                {
                    _logger.LogError("{error}", error.ToString());
                }
                return null!;
            }

            if (!returnPingResult.IsSuccessStatusCode)
            {
                _logger.LogWarning("{statusCode}", returnPingResult.StatusCode);
                return null!;
            }

            var result = JsonSerializer.Deserialize<ReturnRequestPing>(content, _jsonSerializerOptions);

            return result!;
        }
        catch (Exception ex)
        {
            _logger.LogError("{ex}", ex.StackTrace);
        }
        return null!;
    }
}
