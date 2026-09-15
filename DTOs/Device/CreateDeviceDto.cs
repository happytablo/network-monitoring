using System.ComponentModel.DataAnnotations;

namespace a_webapi.DTOs.Device;

public record CreateDeviceDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; init; }  = string.Empty;

    [Required]
    public string IpAddress { get; init; } = string.Empty;
    
    public bool IsOnline { get; init; }
}