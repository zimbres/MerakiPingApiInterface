namespace MerakiPingApiInterface.Services;

public class StartupLoggingService
{
    private readonly ILogger<StartupLoggingService> _logger;

    public StartupLoggingService(ILogger<StartupLoggingService> logger)
    {
        _logger = logger;
    }

    public void Log()
    {
        _logger.LogWarning("App version: {version}", Assembly.GetExecutingAssembly().GetName().Version!.ToString());
    }
}
