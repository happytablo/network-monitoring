namespace a_webapi.DTOs.Device;

public record DeviceStatusHistoryDto
{
    public int Id { get; init; }
    public bool IsOnline { get; init; }
    public DateTime LastUpdate { get; init; }
    public int? ResponseTimeMs { get; init; }
    public string? ErrorMessage { get; init; }
    public int DeviceId { get; init; }
}