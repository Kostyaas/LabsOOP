using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class TreeGotoCommand : ICommand
{
    public string Name => "tree goto";

    public string Description => "Выполняет переход до указанного пути";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("path", out string? path))
        {
            return CommandResult.ErrorResult("Не указан путь к файлу");
        }

        string newPath;
        if (context.FileSystem.IsAbsolutePath(path))
        {
            newPath = context.FileSystem.NormalizePath(path);
        }
        else
        {
            newPath = context.FileSystem.CombinePath(context.CurrentPath, path);
            newPath = context.FileSystem.NormalizePath(newPath);
        }

        if (!context.FileSystem.Exists(newPath))
        {
            return CommandResult.ErrorResult($"Путь не существует: {newPath}");
        }

        if (!context.FileSystem.IsDirectory(newPath))
        {
            return CommandResult.ErrorResult($"Путь не является директорией: {newPath}");
        }

        string oldPath = context.CurrentPath;

        context.CurrentPath = newPath;

        return CommandResult.SuccessResult($"Переход из '{oldPath}' в '{newPath}'");
    }

    public (Dictionary<string, string> Parameters, Dictionary<string, string> Flags) ParseForCommand(string[] args)
    {
        var parameters = new Dictionary<string, string>();
        var flags = new Dictionary<string, string>();
        parameters.Add("path", args[0]);
        return (parameters, flags);
    }
}