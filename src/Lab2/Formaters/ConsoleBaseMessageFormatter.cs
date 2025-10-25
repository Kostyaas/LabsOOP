namespace Itmo.ObjectOrientedProgramming.Lab2.Formaters;

public class ConsoleBaseMessageFormatter : BaseMessageFormatter
{
    public ConsoleBaseMessageFormatter()
        : base(Console.Out) { }
}