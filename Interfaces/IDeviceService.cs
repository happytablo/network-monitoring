using a_webapi.DTOs.Device;
using a_webapi.Models;

namespace a_webapi.Interfaces;

public interface IDeviceService
{
    Task<IReadOnlyList<DeviceDto>> GetAllAsync();

    Task<DeviceDto?> GetByIdAsync(int id);
    Task<IEnumerable<DeviceStatusHistoryDto>> GetHistoryByDeviceIdAsync(int deviceId);

    Task<DeviceDto> CreateAsync(CreateDeviceDto dto);

    Task<bool> UpdateAsync(int id, UpdateDeviceDto dto);

    Task<bool> DeleteAsync(int id);
}