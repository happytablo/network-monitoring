namespace a_webapi.Dto;

public class DeviceQueryDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Name { get; set; }
    public bool? IsOnline { get; set; }
    public string SortBy { get; set; } = "id";
    public string SortDirection { get; set; } = "asc";
    public string? Search { get; set; }
}