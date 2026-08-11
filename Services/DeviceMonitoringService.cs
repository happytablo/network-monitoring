using System.Net.NetworkInformation;
using a_webapi.Data;
using a_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace a_webapi.Services;

public class DeviceMonitoringService
{
    private readonly AppDbContext _dbContext;

    public DeviceMonitoringService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CheckDevicesAsync(CancellationToken cancellationToken)
    {
        var devices = await _dbContext.Devices
            .ToListAsync(cancellationToken);

        Console.WriteLine(
            $"Getting devices done at {DateTime.UtcNow}. " +
            $"Found: {devices.Count}");

        var tasks = devices.Select(device =>
            CheckDeviceAsync(device, cancellationToken));

        var histories = await Task.WhenAll(tasks);

        await _dbContext.DeviceStatusHistories
            .AddRangeAsync(histories, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"Monitoring cycle completed at {DateTime.UtcNow}");
    }

    private static async Task<DeviceStatusHistory> CheckDeviceAsync(Device device, CancellationToken cancellationToken)
    {
        var checkedAt = DateTime.UtcNow;

        using var ping = new Ping();

        try
        {
            var reply = await ping.SendPingAsync(
                device.IpAddress,
                3000);

            var isOnline = reply.Status == IPStatus.Success;

            device.IsOnline = isOnline;
            device.LastCheck = checkedAt;

            Console.WriteLine(
                $"{device.Name} ({device.IpAddress}) " +
                $"Online: {isOnline}, " +
                $"Response: {reply.RoundtripTime} ms");

            return new DeviceStatusHistory
            {
                IsOnline = isOnline,
                LastUpdate = checkedAt,
                DeviceId = device.Id,
                ResponseTimeMs = isOnline
                    ? (int)reply.RoundtripTime
                    : null,
                ErrorMessage = isOnline
                    ? null
                    : $"Ping status: {reply.Status}"
            };
        }
        catch (Exception ex)
        {
            device.IsOnline = false;
            device.LastCheck = checkedAt;

            Console.WriteLine(
                $"Error checking {device.Name}: {ex.Message}");

            return new DeviceStatusHistory
            {
                IsOnline = false,
                LastUpdate = checkedAt,
                DeviceId = device.Id,
                ResponseTimeMs = null,
                ErrorMessage = ex.Message
            };
        }
    }
}