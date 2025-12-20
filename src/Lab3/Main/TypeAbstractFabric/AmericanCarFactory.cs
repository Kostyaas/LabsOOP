using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeCar;
using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeEngine;
using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeTransmission;

namespace Itmo.ObjectOrientedProgramming.Lab3.Main.TypeAbstractFabric;

public class AmericanCarFactory : ICarFactory
{
    public ICar CreateCar() => new Camaro();

    public IEngine CreateEngine() => new AmericanEngine();

    public ITransmission CreateTransmission() => new AmericanTransmission();
}