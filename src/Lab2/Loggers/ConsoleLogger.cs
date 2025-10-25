using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Loggers;

public class ConsoleLogger : ILogger
{
    public void Logger(Message massage)
    {
        Console.WriteLine($"Logger: message :  {massage.Title} -  {massage.Body}");
    }
}