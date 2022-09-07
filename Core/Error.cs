using System.Text.Json;

namespace SimpleService.Core;

public enum ErrorType
{
    ValidationFailed = 1,
    ValueNotFound = 2,
    AccessDenied = 3,
    PermissionMissing = 4,
    NotAllowed = 5,
    InternalError = 6
}

public class Error
{
    public string Message { get; set; }
    public List<string> Details { get; set; }

    public ErrorType ErrorType { get; set; }

    public Error()
    {
        Details = new List<string>();
    }

    public Error(string message) : this()
    {
        Message = message;
    }

    public Error(ErrorType errorType, string message) : this(message)
    {
        ErrorType = errorType;
    }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
    
}