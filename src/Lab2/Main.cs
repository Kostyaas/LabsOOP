using Itmo.ObjectOrientedProgramming.Lab2.Adresses;
using Itmo.ObjectOrientedProgramming.Lab2.Arhivators;
using Itmo.ObjectOrientedProgramming.Lab2.Component;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;
using Itmo.ObjectOrientedProgramming.Lab2.Formaters;
using Itmo.ObjectOrientedProgramming.Lab2.Loggers;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Main
{
    public static void Run()
    {
        var formater = new ConsoleBaseMessageFormatter();
        var arh = new FormattedArchiver(formater);
        var adres = new DecaratorLogger(new ArhivatorAdresses(arh), new ConsoleLogger());
        var people1 = new User("sdfdsf", 123112312);
        var mes = new Message("Ошибка", "Комп сгорел");
        people1.SendMessage(mes, adres);
    }
}