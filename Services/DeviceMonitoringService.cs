namespace a_webapi.Services;

public class DeviceMonitoringService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask; // todo
    }
}