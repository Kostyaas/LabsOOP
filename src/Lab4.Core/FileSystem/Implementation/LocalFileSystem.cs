namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem.Implementation;

public class LocalFileSystem : IFileSystem
{
    public bool Exists(string path)
    {
        return !string.IsNullOrWhiteSpace(path) && (File.Exists(path) || Directory.Exists(path));
    }

    public bool IsFile(string path)
    {
        return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
    }

    public bool IsDirectory(string path)
    {
        return !string.IsNullOrWhiteSpace(path) && Directory.Exists(path);
    }

    public IReadOnlyList<string> ListDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty", nameof(path));

        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory not found: {path}");

        var items = new List<string>();

        IEnumerable<string> directories = Directory.GetDirectories(path)
            .Select(Path.GetFileName)
            .Where(name => name is not null)
            .Cast<string>();

        IEnumerable<string> files = Directory.GetFiles(path)
            .Select(Path.GetFileName)
            .Where(name => name is not null)
            .Cast<string>();
        items.AddRange(files);

        return items.AsReadOnly();
    }

    public string ReadFile(string path)
    {
        return string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty", nameof(path))
            : !File.Exists(path) ? throw new FileNotFoundException($"File not found: {path}") : File.ReadAllText(path);
    }

    public void WriteFile(string path, string content)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty", nameof(path));

        if (content is null)
            throw new ArgumentNullException(nameof(content), "Content cannot be null");

        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, content);
    }

    public void CreateDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty", nameof(path));

        Directory.CreateDirectory(path);
    }

    public void Delete(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty", nameof(path));

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        else
        {
            throw new FileNotFoundException($"Path not found: {path}");
        }
    }

    public void Move(string sourcePath, string destinationPath)
    {
        if (string.IsNullOrWhiteSpace(sourcePath))
            throw new ArgumentException("Source path cannot be null or empty", nameof(sourcePath));

        if (string.IsNullOrWhiteSpace(destinationPath))
            throw new ArgumentException("Destination path cannot be null or empty", nameof(destinationPath));

        if (File.Exists(sourcePath))
        {
            if (Directory.Exists(destinationPath))
            {
                string fileName = Path.GetFileName(sourcePath);
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new InvalidOperationException("Cannot get file name from source path");

                destinationPath = Path.Combine(destinationPath, fileName);
            }

            File.Move(sourcePath, destinationPath);
        }
        else if (Directory.Exists(sourcePath))
        {
            Directory.Move(sourcePath, destinationPath);
        }
        else
        {
            throw new FileNotFoundException($"Source path not found: {sourcePath}");
        }
    }

    public void Copy(string sourcePath, string destinationPath)
    {
        if (string.IsNullOrWhiteSpace(sourcePath))
            throw new ArgumentException("Source path cannot be null or empty", nameof(sourcePath));

        if (string.IsNullOrWhiteSpace(destinationPath))
            throw new ArgumentException("Destination path cannot be null or empty", nameof(destinationPath));

        if (File.Exists(sourcePath))
        {
            if (Directory.Exists(destinationPath))
            {
                string fileName = Path.GetFileName(sourcePath);
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new InvalidOperationException("Cannot get file name from source path");

                destinationPath = Path.Combine(destinationPath, fileName);
            }

            File.Copy(sourcePath, destinationPath, true);
        }
        else if (Directory.Exists(sourcePath))
        {
            CopyDirectory(sourcePath, destinationPath);
        }
        else
        {
            throw new FileNotFoundException($"Source path not found: {sourcePath}");
        }
    }

    public string GetFileName(string path)
    {
        return string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty", nameof(path))
            : Path.GetFileName(path) ?? string.Empty;
    }

    public string GetDirectoryName(string path)
    {
        return string.IsNullOrWhiteSpace(path)
            ? throw new ArgumentException("Path cannot be null or empty", nameof(path))
            : Path.GetDirectoryName(path) ?? string.Empty;
    }

    public string CombinePath(params string[] paths)
    {
        ArgumentNullException.ThrowIfNull(paths);

        return paths.Length == 0 ? string.Empty : Path.Combine(paths);
    }

    public string GetFullPath(string path)
    {
        return string.IsNullOrWhiteSpace(path) ? throw new ArgumentException("Path cannot be null or empty", nameof(path)) : Path.GetFullPath(path);
    }

    public string GetCurrentDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public void SetCurrentDirectory(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty", nameof(path));

        Directory.SetCurrentDirectory(path);
    }

    public bool IsAbsolutePath(string path)
    {
        return !string.IsNullOrWhiteSpace(path) && Path.IsPathRooted(path);
    }

    public string NormalizePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return string.Empty;

        try
        {
            string normalized = Path.GetFullPath(new Uri(path).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return normalized;
        }
        catch (UriFormatException)
        {
            try
            {
                return Path.GetFullPath(path)
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            catch (Exception)
            {
                return path;
            }
        }
    }

    private static void CopyDirectory(string sourceDir, string destDir)
    {
        Directory.CreateDirectory(destDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string fileName = Path.GetFileName(file);
            if (string.IsNullOrWhiteSpace(fileName))
                continue;

            string destFile = Path.Combine(destDir, fileName);
            File.Copy(file, destFile, true);
        }

        foreach (string subDir in Directory.GetDirectories(sourceDir))
        {
            string dirName = Path.GetFileName(subDir);
            if (string.IsNullOrWhiteSpace(dirName))
                continue;

            string destSubDir = Path.Combine(destDir, dirName);
            CopyDirectory(subDir, destSubDir);
        }
    }
}