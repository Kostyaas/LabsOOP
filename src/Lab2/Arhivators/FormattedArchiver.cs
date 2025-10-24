using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Arhivators;

public class FormattedArchiver : IMessageArhivator
{
    private readonly IMessageFormatter _formater;

    public FormattedArchiver(IMessageFormatter formatter)
    {
        _formater = formatter;
    }

    public void Arhivating(Message message)
    {
        _formater.Format(message);
    }
}