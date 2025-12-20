using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<Guid, Account> _accounts = new();
    private readonly Dictionary<string, Account> _accountsByNumber = new();

    public Task<Account?> GetByIdAsync(Guid id)
    {
        _accounts.TryGetValue(id, out Account? account);
        return Task.FromResult(account);
    }

    public Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        _accountsByNumber.TryGetValue(accountNumber, out Account? account);
        return Task.FromResult(account);
    }

    public Task AddAsync(Account account)
    {
        _accounts[account.Id] = account;
        _accountsByNumber[account.AccountNumber] = account;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Account account)
    {
        if (_accounts.ContainsKey(account.Id))
        {
            _accounts[account.Id] = account;
        }

        return Task.CompletedTask;
    }
}