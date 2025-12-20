namespace Itmo.ObjectOrientedProgramming.Lab3.Main;

public interface ICarFactory
{
    ICar CreateCar();

    IEngine CreateEngine();

    ITransmission CreateTransmission();
}