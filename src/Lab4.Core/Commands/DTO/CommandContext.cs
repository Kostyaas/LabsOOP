using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.DTO;

public class CommandContext
{
    public CommandContext(
        string currentPath,
        IFileSystem fileSystem,
        IReadOnlyDictionary<string, string>? parameters,
        IReadOnlyDictionary<string, string>? flags)
    {
        CurrentPath = currentPath ?? throw new ArgumentNullException(nameof(currentPath));
        FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        Parameters = parameters ?? ReadOnlyDictionary<string, string>.Empty;
        Flags = flags ?? ReadOnlyDictionary<string, string>.Empty;
    }

    public string CurrentPath { get; set; }

    public IFileSystem FileSystem { get; set; }

    public IReadOnlyDictionary<string, string> Parameters { get; set; }

    public IReadOnlyDictionary<string, string> Flags { get; set; }
}
