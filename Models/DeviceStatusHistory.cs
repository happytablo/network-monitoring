namespace a_webapi.Models;

public class DeviceStatusHistory
{
    public int Id { get; set; }
    public bool IsOnline { get; set; }
    public DateTime LastUpdate { get; set; }
    public int DeviceId { get; set; }
    public int? ResponseTimeMs { get; set; }
    public string? ErrorMessage { get; set; }
    public Device? Device { get; set; }
}