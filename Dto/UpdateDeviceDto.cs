using System.ComponentModel.DataAnnotations;

namespace a_webapi.Dto;

public class UpdateDeviceDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string IpAddress { get; set; } = string.Empty;
}