using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class FileShowCommand : ICommand
{
    public string[] Pattern { get; } = ["Path"];

    public string Name => "file show";

    public string Description => "Выводит содержимое файла";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("Path", out string? filePath))
        {
            return CommandResult.ErrorResult("Не указан путь к файлу");
        }

        string fullPath = context.FileSystem.IsAbsolutePath(filePath)
            ? context.FileSystem.NormalizePath(filePath)
            : context.FileSystem.NormalizePath(
                context.FileSystem.CombinePath(context.CurrentPath, filePath));

        if (!context.FileSystem.Exists(fullPath)) return CommandResult.ErrorResult("Файл не существует");

        if (!context.FileSystem.IsFile(fullPath)) return CommandResult.ErrorResult("Указанный путь не является файлом");

        string mode = context.Flags.GetValueOrDefault("-m", "console");

        if (mode != "console") return CommandResult.ErrorResult($"Режим '{mode}' не поддерживается");

        try
        {
            string content = context.FileSystem.ReadFile(fullPath);
            return CommandResult.SuccessResult(content);
        }
        catch (IOException ex)
        {
            return CommandResult.ErrorResult($"Ошибка чтения файла: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            return CommandResult.ErrorResult("Нет доступа к файлу");
        }
    }
}