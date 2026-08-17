// Exceptions/ConflictException.cs
namespace TaskTrackerApi.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}