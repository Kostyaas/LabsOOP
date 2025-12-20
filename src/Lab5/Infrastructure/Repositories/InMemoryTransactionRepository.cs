using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;

public class InMemoryTransactionRepository : ITransactionRepository
{
#pragma warning disable CA1823
    private readonly Dictionary<Guid, List<Transaction>> _transactionsByAccount = new();
#pragma warning restore CA1823

    public Task AddAsync(Transaction transaction)
    {
        if (!_transactionsByAccount.ContainsKey(transaction.AccountId))
        {
            _transactionsByAccount[transaction.AccountId] = new List<Transaction>();
        }

        _transactionsByAccount[transaction.AccountId].Add(transaction);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId)
    {
        if (_transactionsByAccount.TryGetValue(accountId, out List<Transaction>? transactions))
        {
            return Task.FromResult<IEnumerable<Transaction>>(transactions.OrderByDescending(t => t.Timestamp));
        }

        return Task.FromResult(Enumerable.Empty<Transaction>());
    }
}