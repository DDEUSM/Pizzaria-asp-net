public class FieldError
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}

public class ApiException: Exception
{    
    public int? StatusCode { get; set; }
    public List<FieldError>? FieldErrors { get; set; }
    public ApiException (
        string? message,
        int? statusCode
        ): base(message)
    {
        StatusCode = statusCode;
    }

     public ApiException (
        string? message,
        int? statusCode,
        List<FieldError> fieldErrors
        ): base(message)
    {
        StatusCode = statusCode;
        FieldErrors = fieldErrors;
    }
}