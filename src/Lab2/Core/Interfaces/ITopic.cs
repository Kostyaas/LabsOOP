using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;

public interface ITopic
{
    string Name { get; }

    void AddAddress(IAdress adress);

    void SendMessageToAddressees(Message message);
}