namespace Itmo.ObjectOrientedProgramming.Lab2.Formaters;

public class FileMessageFormatter : BaseMessageFormatter
{
    public FileMessageFormatter(string filePath)
        : base(new StreamWriter(filePath) { AutoFlush = true })
    {
    }
}