namespace MerakiPingApiInterface.Models;

public class Result
{
    public Data Data { get; set; } = new Data();
}

public class Data
{
    public bool Health { get; set; } = true;
    public int Success { get; set; }
}