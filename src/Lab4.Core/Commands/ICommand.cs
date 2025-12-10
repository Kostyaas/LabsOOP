using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public interface ICommand
{
    string[] Pattern { get; }

    string Name { get; }

    string Description { get; }

    CommandResult Execute(CommandContext context);
}