using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Implementation;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing.DTO;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Файловый менеджер ===");
        Console.WriteLine("Введите 'help' для списка команд");
        Console.WriteLine("Введите 'exit' для выхода\n");

        var fileSystem = new LocalFileSystem();
        var factory = new CommandFactory();
        var parser = new CommandParser(factory);
        string? currentPath = null;
        bool isConnected = false;

        while (true)
        {
            try
            {
                string prompt = isConnected ? $"{currentPath}> " : "not connected> ";
                Console.Write(prompt);

                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    ShowHelp(factory);
                    continue;
                }

                ParsedCommand? parsed = parser.Parse(input);
                if (parsed == null)
                {
                    Console.WriteLine("Неизвестная команда. Введите 'help' для списка команд.\n");
                    continue;
                }

                var context = new CommandContext(
                    currentPath ?? string.Empty,
                    fileSystem,
                    parsed.Parameters,
                    parsed.Flags);

                CommandResult result = parsed.Command.Execute(context);

                if (parsed.Command is ConnectCommand && result.Success)
                {
                    currentPath = context.CurrentPath;
                    isConnected = true;
                }
                else if (parsed.Command is DisconnectCommand && result.Success)
                {
                    currentPath = null;
                    isConnected = false;
                }
                else if (result.Success && !string.IsNullOrEmpty(context.CurrentPath))
                {
                    currentPath = context.CurrentPath;
                }

                Console.WriteLine(result.Message);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}\n");
            }
        }

        Console.WriteLine("Работа завершена.");
    }

    private static void ShowHelp(CommandFactory factory)
    {
        Console.WriteLine("Доступные команды:");
        Console.WriteLine("connect [Address] [-m Mode]  - подключение к ФС");
        Console.WriteLine("disconnect                   - отключение от ФС");
        Console.WriteLine("tree goto [Path]             - переход по директориям");
        Console.WriteLine("tree list [-d Depth]         - вывод дерева директорий");
        Console.WriteLine("file show [Path] [-m Mode]   - просмотр файла");
        Console.WriteLine("file move [Source] [Dest]    - перемещение файла");
        Console.WriteLine("file copy [Source] [Dest]    - копирование файла");
        Console.WriteLine("file delete [Path]           - удаление файла");
        Console.WriteLine("file rename [Path] [Name]    - переименование файла");
        Console.WriteLine();
    }
}