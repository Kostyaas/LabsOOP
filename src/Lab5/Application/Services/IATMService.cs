using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public interface IATMService
{
    Task<Guid> CreateUserSessionAsync(string accountNumber, string pin);

    Task<Guid> CreateAdminSessionAsync(string adminPassword);

    Task<Guid> CreateAccountAsync(string accountNumber, string pin, Guid sessionId);

    Task<decimal> GetBalanceAsync(Guid accountId, Guid sessionId);

    Task<decimal> WithdrawAsync(Guid accountId, decimal amount, Guid sessionId);

    Task<decimal> DepositAsync(Guid accountId, decimal amount, Guid sessionId);

    Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(Guid accountId, Guid sessionId);
}