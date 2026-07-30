using a_webapi.Data;
using a_webapi.Dto;
using a_webapi.Interfaces;
using a_webapi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace a_webapi.Services;

public class DeviceService(AppDbContext dbContext) : IDeviceService
{
    public async Task<IReadOnlyList<DeviceDto>> GetAllAsync()
    {
        return await dbContext.Devices
            .AsNoTracking()
            .ProjectToType<DeviceDto>()
            .ToListAsync();
    }

    public async Task<DeviceDto?> GetByIdAsync(int id)
    {
        return await dbContext.Devices
            .AsNoTracking()
            .Where(d => d.Id == id)
            .ProjectToType<DeviceDto>()
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<DeviceStatusHistoryDto>> GetHistoryByDeviceIdAsync(int deviceId)
    {
        return await dbContext.DeviceStatusHistories
            .Where(h => h.DeviceId == deviceId)
            .OrderByDescending(h => h.LastUpdate)
            .ProjectToType<DeviceStatusHistoryDto>()
            .ToListAsync();
    }

    public async Task<DeviceDto> CreateAsync(CreateDeviceDto dto)
    {
        var device = new Device
        {
            Name = dto.Name,
            IsOnline = dto.IsOnline,
            IpAddress = dto.IpAddress,
            StatusHistory = new List<DeviceStatusHistory> { }
        };

        var initialHistory = new DeviceStatusHistory
        {
            IsOnline = device.IsOnline,
            LastUpdate = DateTime.UtcNow,
        };

        device.StatusHistory.Add(initialHistory);

        await dbContext.Devices.AddAsync(device);
        await dbContext.SaveChangesAsync();

        return device.Adapt<DeviceDto>();
    }

    public async Task<bool> UpdateAsync(int id, UpdateDeviceDto dto)
    {
        var device = await dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (device == null)
            return false;

        device.Name = dto.Name;
        device.IpAddress = dto.IpAddress;

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var device = await dbContext.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (device == null)
            return false;

        dbContext.Devices.Remove(device);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await dbContext.Devices.AnyAsync(d => d.Id == id);
    }
}