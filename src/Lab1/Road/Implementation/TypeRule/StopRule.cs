using Lab1.Trasport.Implementation;

namespace Lab1.Road.Implementation.TypeRule;

public class StopRule : IRule<Train>
{
    public StopRule(int value)
    {
        Value = value;
    }

    public bool Apply(Train transport)
    {
        return transport.Speed <= Value;
    }

    public int Value { get; }
}
