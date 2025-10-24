using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Formaters;

public abstract class BaseMessageFormatter : IMessageFormatter
{
    protected TextWriter Output { get; }

    protected BaseMessageFormatter(TextWriter outputWriter)
    {
        Output = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
    }

    public virtual void Format(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        WriteTitle(message.Title);
        WriteBody(message.Body);
    }

    public virtual void WriteTitle(string title)
    {
        if (string.IsNullOrEmpty(title)) return;

        Output.WriteLine($"# {title}");
        Output.WriteLine();
    }

    public virtual void WriteBody(string body)
    {
        if (string.IsNullOrEmpty(body)) return;

        Output.WriteLine(body);
        Output.WriteLine();
    }

    public virtual void Dispose()
    {
        Output?.Dispose();
    }
}