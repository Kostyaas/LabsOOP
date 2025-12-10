using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class DisconnectCommand : ICommand
{
    public string[] Pattern { get; } = [];

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
}