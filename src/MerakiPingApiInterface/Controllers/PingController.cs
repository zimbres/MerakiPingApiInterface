namespace MerakiPingApiInterface.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    private readonly ILogger<PingController> _logger;
    private readonly PingService _pingService;

    public PingController(ILogger<PingController> logger, PingService pingService)
    {
        _logger = logger;
        _pingService = pingService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string deviceSerial, string target)
    {
        var result = await _pingService.Execute(deviceSerial, target);
        if (result.Data.Success == 9999)
        {
            return Ok(new Health());
        }
        return Ok(result);
    }
}
