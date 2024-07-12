namespace MerakiPingApiInterface.Controllers;

[ApiController]
[Route("/")]
public class HealthController : ControllerBase
{
    private readonly ILogger<PingController> _logger;

    public HealthController(ILogger<PingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<Health> Get()
    {
        return await Task.FromResult(new Health());
    }
}
