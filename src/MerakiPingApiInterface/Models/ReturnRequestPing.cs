namespace MerakiPingApiInterface.Models;

public class ReturnRequestPing
{
    public string? PingId { get; set; }
    public string? Url { get; set; }
    public PingRequest? Request { get; set; }
    public string? Status { get; set; }
}
