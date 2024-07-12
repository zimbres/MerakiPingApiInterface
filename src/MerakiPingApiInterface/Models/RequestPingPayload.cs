namespace MerakiPingApiInterface.Models;

public class RequestPingPayload
{
    public string Target { get; set; } = string.Empty;
    public int Count { get; set; }
}
