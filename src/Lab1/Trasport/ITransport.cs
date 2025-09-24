namespace Lab1.Trasport;

public interface ITransport
{
    int Speed { get; }

    int Weight { get; }

    bool Move();
}

public abstract class TransportBase : ITransport
{
    public int Speed { get; private set; }

    public int Weight { get; }

    protected TransportBase(int weight)
    {
        Weight = weight <= 0 ? 1 : weight;
        Speed = 0;
    }

    public abstract bool Move();

    protected virtual void Accelerate(int acceleration, int time)
    {
        Speed += acceleration * time;
        if (Speed < 0) Speed = 0;
    }
}