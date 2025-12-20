using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class FileRenameCommand : ICommand
{
    public string[] Pattern { get; } = ["Path", "Name"];

    public string Name => "file rename";

    public string Description => "Переименовывает файл";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("Path", out string? path))
            return CommandResult.ErrorResult("Не указан путь к файлу");

        if (!context.Parameters.TryGetValue("Name", out string? newName))
            return CommandResult.ErrorResult("Не указано новое имя");

        if (string.IsNullOrWhiteSpace(newName))
            return CommandResult.ErrorResult("Новое имя не может быть пустым");

        if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            return CommandResult.ErrorResult($"Новое имя содержит недопустимые символы");

        if (newName.Contains(Path.DirectorySeparatorChar, StringComparison.Ordinal) ||
            newName.Contains(Path.AltDirectorySeparatorChar, StringComparison.Ordinal))
        {
            return CommandResult.ErrorResult("Новое имя не должно содержать разделителей пути");
        }

        string resolvedPath = ResolvePath(context, path);

        if (!context.FileSystem.Exists(resolvedPath))
            return CommandResult.ErrorResult($"Файл не существует: {path}");

        if (!context.FileSystem.IsFile(resolvedPath))
            return CommandResult.ErrorResult($"Указанный путь не является файлом: {path}");

        string directory = context.FileSystem.GetDirectoryName(resolvedPath);
        string newPath = context.FileSystem.CombinePath(directory, newName);

        if (context.FileSystem.NormalizePath(resolvedPath) ==
            context.FileSystem.NormalizePath(newPath))
        {
            return CommandResult.ErrorResult("Новое имя совпадает со старым");
        }

        if (context.FileSystem.Exists(newPath))
            return CommandResult.ErrorResult($"Файл с именем '{newName}' уже существует");

        try
        {
            context.FileSystem.Move(resolvedPath, newPath);
            return CommandResult.SuccessResult(
                $"Файл переименован из '{Path.GetFileName(resolvedPath)}' в '{newName}'");
        }
        catch (Exception ex)
        {
            return CommandResult.ErrorResult($"Ошибка при переименовании: {ex.Message}");
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