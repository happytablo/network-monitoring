using a_webapi.Configuration;
using a_webapi.Services;
using Microsoft.Extensions.Options;

namespace a_webapi.BackgroundServices;

public class DeviceMonitoringBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly MonitoringOptions _options;

    public DeviceMonitoringBackgroundService(IServiceScopeFactory serviceScopeFactory, IOptions<MonitoringOptions> options)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"Monitoring started: {DateTime.Now}");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var monitoringService = scope.ServiceProvider.GetRequiredService<DeviceMonitoringService>();

            await monitoringService.CheckDevicesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(_options.IntervalSeconds), stoppingToken);
        }
    }
}