using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Component;

public class User : IUser, IComponent
{
    public int Uidd { get; }

    public string Name { get; }

    private readonly List<UserMessage> _messages = [];

    public IReadOnlyList<UserMessage> Messages => _messages.AsReadOnly();

    public User(string name, int uidd)
    {
        Name = name;
        Uidd = uidd;
    }

    public void SendMessage(Message message, IAdress adress)
    {
        adress.GetMessage(message);
    }

    public void ReadMessage(int messageIndex)
    {
        if (messageIndex >= 0 && messageIndex < _messages.Count)
        {
            _messages[messageIndex].Read();
        }
    }

    public void GetMessage(Message message)
    {
        _messages.Add(new UserMessage(message));
    }
}