namespace SimpleService.Core;

public class UnexpectedResultException : Exception
{
    public UnexpectedResultException(string message) : base(message)
    {
    }
}