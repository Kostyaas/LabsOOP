namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature;

public interface ICreaturePrototype<out T>
{
    T Clone();
}