namespace MerakiPingApiInterface.Services;

public class PingService
{
    private readonly ILogger<PingService> _logger;
    private readonly RequestResultService _resultService;

    public PingService(ILogger<PingService> logger, RequestResultService resultService)
    {
        _logger = logger;
        _resultService = resultService;
    }

    public async Task<Result> Execute(string device, string target)
    {
        var returnResultPing = await _resultService.Execute(device, target);
        var result = new Result
        {
            Data = new Data
            {
                Health = true,
                Success = returnResultPing is null ? 9999 : (int)returnResultPing.Results!.Latencies!.Average,
            }
        };
        return result;
    }
}
