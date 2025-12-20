using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class ConnectCommand : ICommand
{
    public string[] Pattern { get; } = ["Address"];

    public string Name => "connect";

    public string Description => "Подключает систему к файловой системе";

    public CommandResult Execute(CommandContext context)
    {
        string mode = context.Flags.GetValueOrDefault("-m", "local");

        string address = context.Parameters["Address"];

        Console.WriteLine(address);

        context.CurrentPath = context.FileSystem.NormalizePath(address);
        return CommandResult.SuccessResult($"Подключено к{address}");
    }
}