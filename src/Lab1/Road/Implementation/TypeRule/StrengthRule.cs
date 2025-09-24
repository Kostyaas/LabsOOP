using Lab1.Trasport.Implementation;

namespace Lab1.Road.Implementation.TypeRule;

public class StrengthRule : IRule<Train>
{
    public int Value { get; }

    public StrengthRule(int value)
    {
        Value = value;
    }

    public bool Apply(Train transport)
    {
        transport.SetPower(Value);
        return true;
    }
}