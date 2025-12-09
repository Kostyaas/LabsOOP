using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing.DTO;

public class ParsedCommand
{
    public ParsedCommand(ICommand command, Dictionary<string, string> parameters, Dictionary<string, string> flags)
    {
        Command = command;
        Parameters = parameters.AsReadOnly();
        Flags = flags.AsReadOnly();
    }

    public ICommand Command { get; set; }

    public ReadOnlyDictionary<string, string> Parameters { get; set; }

    public ReadOnlyDictionary<string, string> Flags { get; set; }
}