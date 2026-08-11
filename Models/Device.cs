namespace a_webapi.Models;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public DateTime LastCheck { get; set; } = DateTime.UtcNow;
    public List<DeviceStatusHistory>? StatusHistory { get; set; }
}