using Itmo.ObjectOrientedProgramming.Lab2.Core.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab2.Core.Models;

namespace Itmo.ObjectOrientedProgramming.Lab2.Adresses;

public class DecaratorLogger : IAdress
{
    private readonly IAdress _adresses;
    private readonly ILogger _logger;

    public DecaratorLogger(IAdress adress, ILogger logger)
    {
        _adresses = adress;
        _logger = logger;
    }

    public void GetMessage(Message message)
    {
        _logger.Logger(message);
        _adresses.GetMessage(message);
    }
}