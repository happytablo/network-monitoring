namespace a_webapi.DTOs.Device;

public record DeviceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IpAddress { get; init; } = string.Empty;
}