using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;

public interface INotificationSystem
{
    void Notify(Message message);
}