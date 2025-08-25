namespace BuildingBlock.Shared.ValueModels;

public class ApiResponse
{
    public required int Code { get; set; }
    
    public string? Message { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}