using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeCar;
using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeEngine;
using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeTransmission;

namespace Itmo.ObjectOrientedProgramming.Lab3.Main.TypeAbstractFabric;

public class RussianCarFactory : ICarFactory
{
    public ICar CreateCar() => new LadaVesta();

    public IEngine CreateEngine() => new RussianEngine();

    public ITransmission CreateTransmission() => new RussianTransmission();
}