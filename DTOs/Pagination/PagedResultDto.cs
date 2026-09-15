namespace a_webapi.DTOs.Pagination;

public record PagedResultDto<T>
{
    public IEnumerable<T> Items { get; init; } = [];

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }
}