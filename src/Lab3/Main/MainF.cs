using Itmo.ObjectOrientedProgramming.Lab3.Main.Factory;
using Itmo.ObjectOrientedProgramming.Lab3.Main.TypeAbstractFabric;

namespace Itmo.ObjectOrientedProgramming.Lab3.Main;

public class MainF
{
    public static void Main1()
    {
        CarFactory russianFactory = new LadaFactory();
        CarFactory americanFactory = new CamaroFactory();

        ICar lada = russianFactory.CreateCar();
        ICar camaro = americanFactory.CreateCar();

        var abstractRussianFactory = new RussianCarFactory();
        var abstractAmericanFactory = new AmericanCarFactory();

        ICar carlada = abstractRussianFactory.CreateCar();
        IEngine engineruss = abstractRussianFactory.CreateEngine();
        ITransmission americantrans = abstractAmericanFactory.CreateTransmission();
        Console.WriteLine(carlada.GetType().Name);
        Console.WriteLine(engineruss.GetType().Name);
        Console.WriteLine(americantrans.GetType().Name);
    }
}