namespace a_webapi.DTOs.Device;

public record DeviceQueryDto
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Name { get; init; }
    public bool? IsOnline { get; init; }
    public string SortBy { get; init; } = "id";
    public string SortDirection { get; init; } = "asc";
    public string? Search { get; init; }
}