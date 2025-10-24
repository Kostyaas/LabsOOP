using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class MockNotificationSystem : INotificationSystem
{
    private readonly List<Message> _notifiedMessages = new();

    public ReadOnlyCollection<Message> NotifiedMessages => _notifiedMessages.AsReadOnly();

    public int CallCount { get; private set; }

    public void Notify(Message message)
    {
        _notifiedMessages.Add(message);
        CallCount++;
    }
}