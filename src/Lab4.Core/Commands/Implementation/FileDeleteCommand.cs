using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class FileDeleteCommand : ICommand
{
    public string[] Pattern { get; } = ["Path"];

    public string Name => "file delete";

    public string Description => "Удаляет указанный файл";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("Path", out string? path))
            return CommandResult.ErrorResult("Не указан путь к файлу");

        string resolvedPath = ResolvePath(context, path);

        if (!context.FileSystem.Exists(resolvedPath))
            return CommandResult.ErrorResult($"Файл не существует: {path}");

        if (!context.FileSystem.IsFile(resolvedPath))
            return CommandResult.ErrorResult($"Указанный путь не является файлом: {path}");

        try
        {
            string fileName = context.FileSystem.GetFileName(resolvedPath);

            context.FileSystem.Delete(resolvedPath);
            return CommandResult.SuccessResult($"Файл '{fileName}' удален");
        }
        catch (Exception ex)
        {
            return CommandResult.ErrorResult($"Ошибка при удалении: {ex.Message}");
        }
    }

    private string ResolvePath(CommandContext context, string path)
    {
        if (context.FileSystem.IsAbsolutePath(path))
            return context.FileSystem.NormalizePath(path);

        return context.FileSystem.NormalizePath(
            context.FileSystem.CombinePath(context.CurrentPath, path));
    }
}