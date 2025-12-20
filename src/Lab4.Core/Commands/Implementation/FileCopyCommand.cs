using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class FileCopyCommand : ICommand
{
    public string[] Pattern { get; } = ["SourcePath", "DestinationPath"];

    public string Name => "file copy";

    public string Description => "Копирует файл из исходной локации в указанную";

    public CommandResult Execute(CommandContext context)
    {
        if (!context.Parameters.TryGetValue("SourcePath", out string? sourcePath))
            return CommandResult.ErrorResult("Не указан исходный путь");

        if (!context.Parameters.TryGetValue("DestinationPath", out string? destinationPath))
            return CommandResult.ErrorResult("Не указан путь назначения");

        // Разрешаем пути
        string resolvedSource = ResolvePath(context, sourcePath);
        string resolvedDestination = ResolvePath(context, destinationPath);

        // Проверяем существование источника
        if (!context.FileSystem.Exists(resolvedSource))
            return CommandResult.ErrorResult($"Исходный путь не существует: {sourcePath}");

        // Если источник - директория
        if (context.FileSystem.IsDirectory(resolvedSource))
            return CommandResult.ErrorResult("Копирование директорий не поддерживается");

        // Если назначение - директория, добавляем имя файла
        if (context.FileSystem.IsDirectory(resolvedDestination))
        {
            string fileName = context.FileSystem.GetFileName(resolvedSource);
            resolvedDestination = context.FileSystem.CombinePath(resolvedDestination, fileName);
        }

        // Обработка коллизий имен
        string finalDestination = HandleNameCollision(
            context.FileSystem, resolvedDestination);

        try
        {
            context.FileSystem.Copy(resolvedSource, finalDestination);
            return CommandResult.SuccessResult(
                $"Файл скопирован из '{sourcePath}' в '{Path.GetFileName(finalDestination)}'");
        }
        catch (Exception ex)
        {
            return CommandResult.ErrorResult($"Ошибка при копировании: {ex.Message}");
        }
    }

    private string ResolvePath(CommandContext context, string path)
    {
        if (context.FileSystem.IsAbsolutePath(path))
            return context.FileSystem.NormalizePath(path);

        return context.FileSystem.NormalizePath(
            context.FileSystem.CombinePath(context.CurrentPath, path));
    }

    private string HandleNameCollision(IFileSystem fs, string destinationPath)
    {
        if (!fs.Exists(destinationPath))
            return destinationPath;

        string dir = fs.GetDirectoryName(destinationPath);
        string name = Path.GetFileNameWithoutExtension(destinationPath);
        string ext = Path.GetExtension(destinationPath);
        int counter = 1;

        while (fs.Exists(destinationPath))
        {
            destinationPath = fs.CombinePath(
                dir, $"{name}_copy_{counter}{ext}");
            counter++;
        }

        return destinationPath;
    }
}