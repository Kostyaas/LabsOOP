using Itmo.ObjectOrientedProgramming.Lab5.Domain.Enums;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid AccountId { get; private set; }

    public TransactionType Type { get; private set; }

    public decimal Amount { get; private set; }

    public decimal BalanceAfter { get; private set; }

    public DateTime Timestamp { get; private set; }

    public Transaction(Guid accountId, TransactionType type, decimal amount, decimal balanceAfter)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;
        Timestamp = DateTime.UtcNow;
    }
}