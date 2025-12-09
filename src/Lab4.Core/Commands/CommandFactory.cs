using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public class CommandFactory
{
    private readonly Dictionary<string, Type> _commandTypes = new();

    public IEnumerable<string> GetAvailableCommands()
    {
        return _commandTypes.Keys;
    }

    public CommandFactory()
    {
        RegisterCommand<ConnectCommand>("connect");
        RegisterCommand<DisconnectCommand>("disconnect");
        RegisterCommand<TreeGotoCommand>("tree goto");
        RegisterCommand<TreeListCommand>("tree list");
        RegisterCommand<FileShowCommand>("file show");

        // RegisterCommand<FileMoveCommand>("file move");
        // RegisterCommand<FileCopyCommand>("file copy");
        // RegisterCommand<FileDeleteCommand>("file delete");
        // RegisterCommand<FileRenameCommand>("file rename");
    }

    public ICommand? CreateCommand(string name)
    {
        if (_commandTypes.TryGetValue(name, out Type? type))
        {
            return Activator.CreateInstance(type) as ICommand;
        }

        return null;
    }

    private void RegisterCommand<T>(string name) where T : ICommand, new()
    {
        _commandTypes[name] = typeof(T);
    }
}