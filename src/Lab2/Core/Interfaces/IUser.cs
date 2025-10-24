using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;

public interface IUser
{
    string Name { get; }

    void SendMessage(Message message, IAdress adress);
}