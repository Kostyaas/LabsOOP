namespace Lab1.Trasport.Implementation;

public class Train : ITransport
{
    public int Speed { get; private set; }

    public int Weight { get; }

    private int MaxStrength { get; }

    private int CurrentPower { get; set; }

    public Train(int weight = 1, int maxStrength = 0)
    {
        Weight = weight <= 0 ? 1 : weight;
        Speed = 0;
        MaxStrength = maxStrength;
    }

    public bool IsSetPower(int strength)
    {
        if (strength > MaxStrength)
        {
            CurrentPower = MaxStrength;
            return false;
        }

        CurrentPower = strength;
        return true;
    }

    public bool IsMove()
    {
        int acceleration = CurrentPower / Weight;
        Accelerate(acceleration, 1);
        return Speed > 0;
    }

    private void Accelerate(int acceleration, int time)
    {
        Speed += acceleration * time;
        if (Speed < 0)
            Speed = 0;
    }
}