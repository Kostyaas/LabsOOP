using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class MockMessageFormatter : IMessageFormatter
{
    private readonly List<Message> _formattedMessages = [];
    private readonly List<string> _writtenTitles = [];
    private readonly List<string> _writtenBodies = [];

    public ReadOnlyCollection<Message> FormattedMessages => _formattedMessages.AsReadOnly();

    public ReadOnlyCollection<string> WrittenTitles => _writtenTitles.AsReadOnly();

    public ReadOnlyCollection<string> WrittenBodies => _writtenBodies.AsReadOnly();

    public int FormatCallCount { get; set; }

    public int WriteTitleCallCount { get; set; }

    public int WriteBodyCallCount { get; set; }

    public void Format(Message message)
    {
        _formattedMessages.Add(message);
        FormatCallCount++;
    }

    public void WriteTitle(string title)
    {
        _writtenTitles.Add(title);
        WriteTitleCallCount++;
    }

    public void WriteBody(string body)
    {
        _writtenBodies.Add(body);
        WriteBodyCallCount++;
    }
}