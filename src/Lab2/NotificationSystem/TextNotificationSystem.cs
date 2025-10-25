using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.NotificationSystem;

public class TextNotificationSystem : INotificationSystem
{
    public void Notify(Message message)
    {
        Console.WriteLine(message.ToString());
    }
}