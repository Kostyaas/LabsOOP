using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeCar;

namespace Itmo.ObjectOrientedProgramming.Lab3.Main.Factory;

public class CamaroFactory : CarFactory
{
    public override ICar CreateCar()
    {
        return new Camaro();
    }
}
