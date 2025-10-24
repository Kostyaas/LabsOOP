using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Arhivators;

public class CollectionArhivator : IMessageArhivator
{
    private readonly List<Message> _messages = new();

    public IReadOnlyList<Message> Messages => _messages.AsReadOnly();

    public void Arhivating(Message message)
    {
        _messages.Add(message);
    }

    public void Arhivating(IEnumerable<Message> messages)
    {
        _messages.AddRange(messages);
    }

    public void Clear()
    {
        _messages.Clear();
    }
}