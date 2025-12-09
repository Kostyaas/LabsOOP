using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class ConnectCommand : ICommand
{
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

    public (Dictionary<string, string> Parameters, Dictionary<string, string> Flags) ParseForCommand(string[] args)
    {
        var parameters = new Dictionary<string, string>();
        var flags = new Dictionary<string, string>();
        bool fl = false;
        for (int i = 0; i < args.Length; ++i)
        {
            if (args[i] == "-m")
            {
                fl = true;
            }
            else if (fl)
            {
                flags["-m"] = args[i];
            }
            else
            {
                parameters["Address"] = args[i];
            }
        }

        return (parameters, flags);
    }
}