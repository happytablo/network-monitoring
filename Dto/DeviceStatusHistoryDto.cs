namespace a_webapi.Dto;

public class DeviceStatusHistoryDto
{
    public int Id { get; set; }
    public bool IsOnline { get; set; }
    public DateTime LastUpdate { get; set; }
    public int DeviceId { get; set; }
}