namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

public class CommandResult
{
    public bool Success { get; }

    public string Message { get; }

    public object? Data { get; }

    private CommandResult(bool success, string message, object? data = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static CommandResult SuccessResult(string message = "")
    {
        return new CommandResult(true, message);
    }

    public static CommandResult SuccessResultWithData(string message, object data)
    {
        return new CommandResult(true, message, data);
    }

    public static CommandResult ErrorResult(string message)
    {
        return new CommandResult(false, message);
    }

    public static CommandResult ErrorResultWithData(string message, object data)
    {
        return new CommandResult(false, message, data);
    }
}