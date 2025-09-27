namespace Lab1.Trasport;

public interface ITransport
{
    int Speed { get; }

    int Weight { get; }

    bool IsMove();
}