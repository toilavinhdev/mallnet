namespace BuildingBlock.Shared.ValueModels;

public interface IPaginationRequest
{
    public int PageIndex { get; set; }
    
    public int PageSize { get; set; }
}

public record Pagination
{
    public int PageIndex { get; set; }
    
    public int PageSize { get; set; }
    
    public int TotalItem { get; set; }
    
    public long TotalPage => (long)Math.Ceiling(TotalItem / (double)PageSize);
}

public record PaginationResult<T>
{
    public Pagination? Pagination { get; set; }
    
    public List<T> Items { get; set; } = [];
}