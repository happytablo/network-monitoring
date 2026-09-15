namespace a_webapi.Configuration;

public class MonitoringOptions
{
    public int IntervalSeconds { get; init; }
    public int PingTimeoutMilliseconds { get; init; }
}