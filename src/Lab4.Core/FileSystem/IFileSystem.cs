namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystem;

public interface IFileSystem
{
    bool Exists(string path);

    bool IsFile(string path);

    bool IsDirectory(string path);

    IReadOnlyList<string> ListDirectory(string path);

    string ReadFile(string path);

    void WriteFile(string path, string content);

    void CreateDirectory(string path);

    void Delete(string path);

    void Move(string sourcePath, string destinationPath);

    void Copy(string sourcePath, string destinationPath);

    string GetFileName(string path);

    string GetDirectoryName(string path);

    string CombinePath(params string[] paths);

    string GetFullPath(string path);

    string GetCurrentDirectory();

    void SetCurrentDirectory(string path);

    bool IsAbsolutePath(string path);

    string NormalizePath(string path);
}