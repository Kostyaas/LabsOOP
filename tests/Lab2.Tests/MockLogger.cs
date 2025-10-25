using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class MockLogger : ILogger
{
    private readonly List<Message> _loggedMessages = [];

    public ReadOnlyCollection<Message> LoggedMessages => _loggedMessages.AsReadOnly();

    public int CallCount { get; set; }

    public void Logger(Message massage)
    {
        _loggedMessages.Add(massage);
        CallCount++;
    }
}