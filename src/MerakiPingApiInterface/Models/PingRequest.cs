namespace MerakiPingApiInterface.Models;

public class PingRequest
{
    public string Serial { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
    public int Count { get; set; }
}