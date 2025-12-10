using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing;

public class CommandParser
{
    private readonly CommandFactory _factory;

    public CommandParser(CommandFactory factory)
    {
        _factory = factory;
    }

    public ParsedCommand? Parse(string input)
    {
        string[] parts = SplitInput(input);

        for (int i = parts.Length; i > 0; i--)
        {
            string commandName = string.Join(" ", parts.Take(i));
            ICommand? command = _factory.CreateCommand(commandName);

            if (command != null)
            {
                string[] args = parts.Skip(i).ToArray();
                return ParseArguments(command, args);
            }
        }

        return null;
    }

    private ParsedCommand ParseArguments(ICommand command, string[] args)
    {
        string[] pattern = command.Pattern;
        var flags = new Dictionary<string, string>();

        int currentIndex = 0;
        Dictionary<string, string> parameters = pattern.ToDictionary(arg => arg, arg => args[currentIndex++]);

        for (; currentIndex < args.Length; ++currentIndex)
        {
            if (!args[currentIndex].StartsWith('-')) continue;
            string flagname = args[currentIndex++];
            string flag = args[currentIndex];
            flags.Add(flagname, flag);
        }

        return new ParsedCommand(command, parameters, flags);
    }

    private string[] SplitInput(string input)
    {
        // MatchCollection matches = Regex.Matches(input, @"(?<match>\w+)|\""(?<match>[\w\s\.]+)\""");
        // return matches.Select(m => m.Groups["match"].Value).ToArray();
        return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
}