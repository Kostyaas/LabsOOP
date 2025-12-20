namespace Itmo.ObjectOrientedProgramming.Lab5.Application.DTO;

public class AccountDto
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }
}