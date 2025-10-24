using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Arhivators;

public class FormattedArchiver : IMessageArhivator
{
    private readonly IMessageFormatter formater;

    public FormattedArchiver(IMessageFormatter formatter)
    {
        this.formater = formatter;
    }

    public void Arhivating(Message message)
    {
        formater.Format(message);
    }
}