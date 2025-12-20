using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities.Account;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);

    Task<Account?> GetByAccountNumberAsync(string accountNumber);

    Task AddAsync(Account account);

    Task UpdateAsync(Account account);
}