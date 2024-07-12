namespace MerakiPingApiInterface.Models;

public class ReturnResultPing
{
    public string? PingId { get; set; }
    public string? Url { get; set; }
    public PingRequest? Request { get; set; }
    public string? Status { get; set; }
    public Results? Results { get; set; }
}

public class Results
{
    public int Sent { get; set; }
    public int Received { get; set; }
    public Loss? Loss { get; set; }
    public Latencies? Latencies { get; set; }
    public List<Reply>? Replies { get; set; }
}

public class Loss
{
    public double Percentage { get; set; }
}

public class Latencies
{
    public double Minimum { get; set; }
    public double Average { get; set; }
    public double Maximum { get; set; }
}

public class Reply
{
    public int SequenceId { get; set; }
    public int Size { get; set; }
    public double Latency { get; set; }
}
