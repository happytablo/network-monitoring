using a_webapi.Services;

namespace a_webapi.BackgroundServices;

public class DeviceMonitoringBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DeviceMonitoringBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"Monitoring started: {DateTime.Now}");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var monitoringService = scope.ServiceProvider.GetRequiredService<DeviceMonitoringService>();

            await monitoringService.CheckDevicesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}