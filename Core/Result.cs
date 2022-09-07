namespace SimpleService.Core;


public class Result
{
    public Error Error { get; private set; }

    public List<string> Warnings { get; private set; }

    public bool Succeeded { get; private set; }

    public Result()
    {
        Succeeded = true;
        Warnings = new List<string>();
    }

    public Result(Error error)
    {
        Succeeded = false;
        Error = error;
        Warnings = new List<string>();
    }
    public static Result Failure(ErrorType errorType, string message)
    {
        return new Result(new Error(errorType, message));
    }

    public static Result Success()
    {
        return new Result();
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    public T Unwrap()
    {
        if (Succeeded)
        {
            return Value;
        }

        switch (Error.ErrorType)
        {
            case ErrorType.AccessDenied:
                throw new UnauthorizedAccessException(Error.Message);

            default:
                throw new UnexpectedResultException(Error.Message);
        }
    }

    protected Result(T value)
    {
        Value = value;
    }

    protected Result(Error error) : base(error)
    {
    }
    
    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }
    
    public static Result<T> Failure(ErrorType errorType, string message)
    {
        return new Result<T>(new Error(errorType, message));
    }
}



