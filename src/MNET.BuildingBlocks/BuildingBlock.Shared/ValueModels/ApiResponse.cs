namespace BuildingBlock.Shared.ValueModels;

public class ApiResponse
{
    public string? Message { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}