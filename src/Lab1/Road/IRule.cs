using Lab1.Trasport;

namespace Lab1.Road;

public interface IRule<T> where T : ITransport
{
    int Value { get; }

    bool Apply(T transport);
}
