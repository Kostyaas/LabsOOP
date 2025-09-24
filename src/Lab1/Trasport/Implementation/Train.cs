namespace Lab1.Trasport.Implementation;

public class Train : TransportBase
{
    private int MaxStrength { get; }

    private int CurrentPower { get; set; }

    public Train(int weight, int maxStrength) : base(weight)
    {
        MaxStrength = maxStrength;
    }

    public bool SetPower(int strength)
    {
        if (strength > MaxStrength)
        {
            CurrentPower = MaxStrength;
            return false;
        }

        CurrentPower = strength;
        return true;
    }

    public override bool Move()
    {
        int acceleration = CurrentPower / Weight;
        Accelerate(acceleration, 1);
        Console.WriteLine($"{Speed} \n");
        return Speed > 0;
    }
}