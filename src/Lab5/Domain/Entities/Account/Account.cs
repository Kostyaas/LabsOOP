using Itmo.ObjectOrientedProgramming.Lab5.Domain.Exception;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities.Account;

public class Account
{
    public Guid Id { get; private set; }

    private readonly Pin _pin;

    public string AccountNumber { get; private set; }

    public decimal Balance { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Account(string accountNumber, Pin pin)
    {
        AccountNumber = accountNumber;
        Id = Guid.NewGuid();
        _pin = pin;
        Balance = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (Balance < amount)
            throw new Nicheta(Balance, amount);

        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        Balance += amount;
    }

    public bool VerifyPin(Pin pin) => _pin.Equals(pin);
}