using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public interface ICommand
{
    string Name { get; }

    string Description { get; }

    CommandResult Execute(CommandContext context);

    (Dictionary<string, string> Parameters, Dictionary<string, string> Flags) ParseForCommand(string[] args);
}