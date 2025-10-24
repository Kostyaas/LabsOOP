using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;

public interface IMessageFormatter
{
    void Format(Message message);

    void WriteTitle(string title);

    void WriteBody(string body);
}