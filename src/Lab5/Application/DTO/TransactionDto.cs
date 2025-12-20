namespace Itmo.ObjectOrientedProgramming.Lab5.Application.DTO;

public class TransactionDto
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public string Type { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal BalanceAfter { get; set; }

    public DateTime Timestamp { get; set; }
}