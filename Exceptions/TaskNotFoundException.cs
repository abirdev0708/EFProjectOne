namespace TaskTrackerApi.Exceptions;

public class TaskNotFoundException : Exception
{
    public TaskNotFoundException(int id) 
        : base($"Task with id {id} was not found.") { }
}