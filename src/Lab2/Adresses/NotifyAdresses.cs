using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Adresses;

public class NotifyAdresses
{
    private readonly INotificationSystem _system;
    private readonly IReadOnlyCollection<string> _triggerWords;

    public NotifyAdresses(INotificationSystem system, IEnumerable<string> words)
    {
        _system = system;
        _triggerWords = words?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
    }

    public void Receive(Message message)
    {
        bool hasTriggerWord = _triggerWords.Any(word =>
            message.Body.Contains(word, StringComparison.OrdinalIgnoreCase));

        if (hasTriggerWord)
        {
            _system.Notify(message);
        }
    }
}