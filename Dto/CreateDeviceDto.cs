using System.ComponentModel.DataAnnotations;

namespace a_webapi.Dto;

public class CreateDeviceDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string IpAddress { get; set; } = string.Empty;
    
    public bool IsOnline { get; set; }
}