// Exceptions/UnauthorizedException.cs
namespace TaskTrackerApi.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}