namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Exception;

public class Nicheta : DomainException
{
    public decimal CurrentBalance { get; }

    public decimal RequestedAmount { get; }

    public decimal ShortageAmount => RequestedAmount - CurrentBalance;

    public Nicheta(decimal currentBalance, decimal requestedAmount)
        : base($"Недостаточно средств на счете. Текущий баланс: {currentBalance}, запрошено: {requestedAmount}")
    {
        CurrentBalance = currentBalance;
        RequestedAmount = requestedAmount;
    }

    public Nicheta(string message, decimal currentBalance, decimal requestedAmount)
        : base(message)
    {
        CurrentBalance = currentBalance;
        RequestedAmount = requestedAmount;
    }

    public Nicheta(string message, System.Exception innerException, decimal currentBalance, decimal requestedAmount)
        : base(message, innerException)
    {
        CurrentBalance = currentBalance;
        RequestedAmount = requestedAmount;
    }
}