using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);

    Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId);
}