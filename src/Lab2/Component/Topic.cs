using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Component;

public class Topic : ITopic, IComponent
{
    public int Uidd { get; }

    private readonly List<IAdress> _addressees = [];

    public Topic(int uidd, string name)
    {
        Uidd = uidd;
        Name = name;
    }

    public string Name { get; }

    public void AddAddress(IAdress adress)
    {
        _addressees.Add(adress);
    }

    public void GetMessage(Message message)
    {
        SendMessageToAddressees(message);
    }

    public void SendMessageToAddressees(Message message)
    {
        foreach (IAdress addressee in _addressees)
        {
            addressee.GetMessage(message);
        }
    }
}