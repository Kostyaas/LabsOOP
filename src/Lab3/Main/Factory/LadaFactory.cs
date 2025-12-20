using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeCar;

namespace Itmo.ObjectOrientedProgramming.Lab3.Main.Factory;

public class LadaFactory : CarFactory
{
    public override ICar CreateCar()
    {
        return new LadaVesta();
    }
}