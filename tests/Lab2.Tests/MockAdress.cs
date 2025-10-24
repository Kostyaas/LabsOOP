using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using System.Collections.ObjectModel;
using Message = Itmo.ObjectOrientedProgramming.Lab2.Core.Models.Message;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class MockAdress : IAdress
{
    private readonly List<Message> _receivedMessages = new();

    public ReadOnlyCollection<Message> ReceivedMessages => _receivedMessages.AsReadOnly();

    public int CallCount { get; private set; }

    public void GetMessage(Message message)
    {
        _receivedMessages.Add(message);
        CallCount++;
    }
}