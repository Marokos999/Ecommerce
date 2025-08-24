namespace API.Errors;

public class ApiErrorResponse(string message, int statusCode, string? details = null)
{
    public string Message { get; set; } = message;
    public int StatusCode { get; set; } = statusCode;
    public string? Details { get; set; } = details;
}
