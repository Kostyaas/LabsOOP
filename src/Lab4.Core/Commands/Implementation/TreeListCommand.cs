using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;

public class TreeListCommand : ICommand
{
    public string[] Pattern { get; } = [];

    public string Name => "tree list";

    public string Description => "Выводит содержимое директории в виде дерева";

    public CommandResult Execute(CommandContext context)
    {
        int depth = 1;
        string depthStr = context.Flags["-d"];
        if (int.TryParse(depthStr, out int parsedDepth) && parsedDepth > 0)
        {
            depth = parsedDepth;
        }

        Console.WriteLine(depth);
        string tree = BuildTree(context.FileSystem, context.CurrentPath, depth);
        return CommandResult.SuccessResult(tree);
    }

    private string BuildTree(IFileSystem fs, string path, int depth, int currentDepth = 0, string prefix = "", bool isLast = true)
    {
        var sb = new StringBuilder();

        if (currentDepth == 0) sb.AppendLine(Path.GetFileName(path) + " [DIR]");
        if (currentDepth >= depth) return sb.ToString();

        try
        {
            var items = fs.ListDirectory(path).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                bool lastItem = i == items.Count - 1;
                string currentPrefix = prefix + (lastItem ? "└── " : "├── ");
                string childPrefix = prefix + (lastItem ? "    " : "│   ");

                string itemPath = fs.CombinePath(path, items[i]);

                if (fs.IsDirectory(itemPath))
                {
                    sb.AppendLine($"{currentPrefix}{items[i]} [DIR]");
                    sb.Append(BuildTree(fs, itemPath, depth, currentDepth + 1, childPrefix, lastItem));
                }
                else
                {
                    sb.AppendLine($"{currentPrefix}{items[i]}");
                }
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine($"{prefix}└── [Ошибка: {ex.Message}]");
        }

        return sb.ToString();
    }
}