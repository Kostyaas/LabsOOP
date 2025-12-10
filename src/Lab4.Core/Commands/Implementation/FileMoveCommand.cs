using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class FileMoveCommand : ICommand
{
    public string[] Pattern { get; } = ["SourcePath", "DestinationPath"];

    public string Name => "file move";

    public string Description => "Перемещает файл из исходной локации в указанную";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("SourcePath", out string? sourcePath))
            return CommandResult.ErrorResult("Не указан исходный путь");

        if (!context.Parameters.TryGetValue("DestinationPath", out string? destinationPath))
            return CommandResult.ErrorResult("Не указан путь назначения");

        string resolvedSource = ResolvePath(context, sourcePath);
        string resolvedDestination = ResolvePath(context, destinationPath);

        if (!context.FileSystem.Exists(resolvedSource))
            return CommandResult.ErrorResult($"Исходный путь не существует: {sourcePath}");

        if (context.FileSystem.IsDirectory(resolvedSource))
            return CommandResult.ErrorResult("Перемещение директорий не поддерживается");

        if (context.FileSystem.IsDirectory(resolvedDestination))
        {
            string fileName = context.FileSystem.GetFileName(resolvedSource);
            resolvedDestination = context.FileSystem.CombinePath(resolvedDestination, fileName);
        }

        if (context.FileSystem.NormalizePath(resolvedSource) ==
            context.FileSystem.NormalizePath(resolvedDestination))
        {
            return CommandResult.ErrorResult("Нельзя переместить файл в самого себя");
        }

        if (context.FileSystem.Exists(resolvedDestination))
        {
            string dir = context.FileSystem.GetDirectoryName(resolvedDestination);
            string name = Path.GetFileNameWithoutExtension(resolvedDestination);
            string ext = Path.GetExtension(resolvedDestination);
            int counter = 1;

            while (context.FileSystem.Exists(resolvedDestination))
            {
                resolvedDestination = context.FileSystem.CombinePath(
                    dir, $"{name}_{counter}{ext}");
                counter++;
            }
        }

        try
        {
            context.FileSystem.Move(resolvedSource, resolvedDestination);
            return CommandResult.SuccessResult($"Файл перемещен из '{sourcePath}' в '{destinationPath}'");
        }
        catch (Exception ex)
        {
            return CommandResult.ErrorResult($"Ошибка при перемещении: {ex.Message}");
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