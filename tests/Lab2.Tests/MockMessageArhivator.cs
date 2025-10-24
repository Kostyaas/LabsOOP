using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class MockMessageArhivator : IMessageArhivator
{
    private readonly List<Message> _archivedMessages = new();

    public ReadOnlyCollection<Message> ArchivedMessages => _archivedMessages.AsReadOnly();

    public int CallCount { get; private set; }

    public void Arhivating(Message message)
    {
        _archivedMessages.Add(message);
        CallCount++;
    }
}