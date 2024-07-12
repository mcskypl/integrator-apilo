namespace IntegratorApilo.Shared;

public class ServiceResponseError
{
    public string ErrorInfo { get; set; } = string.Empty;
    public string ErrorException { get; set; } = string.Empty;
    public string? ErrorInnerException { get; set; } = string.Empty;
}

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<ServiceResponseError> Errors { get; set; } = new();
}