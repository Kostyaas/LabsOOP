using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Adresses;

public class ComponentAdres : IAdress
{
    private IComponent CurrentComponent { get; }

    public ComponentAdres(IComponent component)
    {
        CurrentComponent = component;
    }

    public void GetMessage(Message message)
    {
        CurrentComponent.GetMessage(message);
    }
}
