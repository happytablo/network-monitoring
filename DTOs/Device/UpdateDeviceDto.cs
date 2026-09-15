using System.ComponentModel.DataAnnotations;

namespace a_webapi.DTOs.Device;

public record UpdateDeviceDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; init; } = string.Empty;

    [Required]
    public string IpAddress { get; init; } = string.Empty;
}