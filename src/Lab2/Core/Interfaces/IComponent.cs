using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;

public interface IComponent
{
    int Uidd { get; }

    void GetMessage(Message message);
}