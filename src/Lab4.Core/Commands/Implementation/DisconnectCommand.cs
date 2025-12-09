using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class DisconnectCommand : ICommand
{
    public string Name => "Disconnect";

    public string Description => "Отключается от файловой системы";

    public CommandResult Execute(CommandContext context)
    {
        if (string.IsNullOrWhiteSpace(context.CurrentPath))
        {
            return CommandResult.ErrorResult(
                "Нет активного подключения. Сначала выполните команду 'connect'.");
        }

        return CommandResult.SuccessResult("Отключено от файловой системы.");
    }

    public (Dictionary<string, string> Parameters, Dictionary<string, string> Flags) ParseForCommand(string[] args)
    {
        var parameters = new Dictionary<string, string>();
        var flags = new Dictionary<string, string>();

        return (parameters, flags);
    }
}