using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Adresses;

public class ArhivatorAdresses : IAdress
{
    private IMessageArhivator Arhivator { get; }

    public ArhivatorAdresses(IMessageArhivator arhivator)
    {
        this.Arhivator = arhivator;
    }

    public void GetMessage(Message message)
    {
        Arhivator.Arhivating(message);
    }
}