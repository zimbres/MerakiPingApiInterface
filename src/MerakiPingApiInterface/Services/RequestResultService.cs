namespace MerakiPingApiInterface.Services;

public class RequestResultService
{
    private readonly ILogger<RequestResultService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly RequestPingService _pingService;
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RequestResultService(ILogger<RequestResultService> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration, RequestPingService pingService)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        var proxyConfiguration = _configuration.GetSection(nameof(ProxyConfiguration)).Get<ProxyConfiguration>();
        _httpClient = _httpClientFactory.CreateClient(proxyConfiguration!.UseProxy ? "Proxy" : "Default");
        _pingService = pingService;
    }

    public async Task<ReturnResultPing> Execute(string device, string target)
    {
        var merakiApiConfiguration = _configuration.GetSection(nameof(MerakiApiConfiguration)).Get<MerakiApiConfiguration>()!;

        var returnRequestPing = await _pingService.Execute(device, target);

        if (returnRequestPing is null)
        {
            return null!;
        }

        await Task.Delay(merakiApiConfiguration.Count * 10 * 1000);

        try
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {merakiApiConfiguration.Token}");

            var returnPingResult = await _httpClient.GetAsync($"{merakiApiConfiguration.BaseAddress}/{device}/liveTools/ping/{returnRequestPing.PingId}");


            var content = await returnPingResult.Content.ReadAsStringAsync();

            var averageLatency = JsonDocument.Parse(content).RootElement.GetProperty("results").GetProperty("latencies").GetProperty("average").ValueKind;

            if (averageLatency != JsonValueKind.Null)
            {
                var result = JsonSerializer.Deserialize<ReturnResultPing>(content, _jsonSerializerOptions);
                return result!;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("{ex}", ex.StackTrace);
        }
        return null!;
    }
}
