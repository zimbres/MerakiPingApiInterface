namespace MerakiPingApiInterface.Configurations;

public class ProxyConfiguration
{
    public bool UseProxy { get; set; }
    public string? Address { get; set; }
    public int Port { get; set; }
}
