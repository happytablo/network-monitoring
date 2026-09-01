using a_webapi.Data;
using a_webapi.Dto;
using a_webapi.Dto.Pagination;
using a_webapi.Interfaces;
using a_webapi.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace a_webapi.Services;

public class DeviceService(AppDbContext dbContext) : IDeviceService
{
    public async Task<PagedResultDto<DeviceDto>> GetAllAsync(DeviceQueryDto request)
    {
        var query = dbContext.Devices
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            string searchPattern = $"%{request.Search}%";
            query = query.Where(d => EF.Functions.ILike(d.Name, $"%{searchPattern}%"));
        }

        if (request.IsOnline.HasValue)
        {
            query = query.Where(d => d.IsOnline == request.IsOnline.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(d => d.Name.Contains(request.Name));
        }

        query = request.SortBy.ToLower() switch
        {
            "name" => request.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Name)
                : query.OrderBy(d => d.Name),

            "isonline" => request.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(d => d.IsOnline)
                : query.OrderBy(d => d.IsOnline),

            _ => request.SortDirection.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Id)
                : query.OrderBy(d => d.Id)
        };

        var totalCount = await query.CountAsync();

        var devices = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectToType<DeviceDto>()
            .ToListAsync();

        return new PagedResultDto<DeviceDto>
        {
            Items = devices,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };
    }

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