using Itmo.ObjectOrientedProgramming.Lab2.Core.Enums;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Adresses;

public class ProxyFilter : IAdress
{
    private readonly IAdress _adresses;
    private readonly MessagePriority _priority;

    public ProxyFilter(IAdress adress, MessagePriority priority)
    {
        _adresses = adress;
        _priority = priority;
    }

    public void GetMessage(Message message)
    {
        if (message.Priority >= _priority)
        {
            _adresses.GetMessage(message);
        }
    }
}