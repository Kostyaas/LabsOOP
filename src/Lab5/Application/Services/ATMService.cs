using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Enums;
using Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Services;

public class ATMService : IATMService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly string _adminPassword;

    public ATMService(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        ISessionRepository sessionRepository,
        string adminPassword)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
        _sessionRepository = sessionRepository;
        _adminPassword = adminPassword;
    }

    public async Task<Guid> CreateUserSessionAsync(string accountNumber, string pin)
    {
        Account? account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null || !account.VerifyPin(new Pin(pin)))
            throw new UnauthorizedAccessException("Invalid account number or PIN");

        var session = new Session(account.Id, false);
        await _sessionRepository.AddAsync(session);
        return session.Id;
    }

    public async Task<Guid> CreateAdminSessionAsync(string adminPassword)
    {
        if (adminPassword != _adminPassword)
            throw new UnauthorizedAccessException("Invalid admin password");

        var session = new Session(null, true);
        await _sessionRepository.AddAsync(session);
        return session.Id;
    }

    public async Task<Guid> CreateAccountAsync(string accountNumber, string pin, Guid sessionId)
    {
        Session session = await ValidateSessionAsync(sessionId);
        if (!session.IsAdmin)
            throw new UnauthorizedAccessException("Admin rights required");

        Account? existingAccount = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (existingAccount != null)
            throw new InvalidOperationException("Account already exists");

        var account = new Account(accountNumber, new Pin(pin));
        await _accountRepository.AddAsync(account);
        return account.Id;
    }

    public async Task<decimal> GetBalanceAsync(Guid accountId, Guid sessionId)
    {
        (Session _, Account account) = await ValidateAccountAccessAsync(accountId, sessionId);

        var transaction = new Transaction(accountId, TransactionType.BalanceCheck, 0, account.Balance);
        await _transactionRepository.AddAsync(transaction);

        return account.Balance;
    }

    public async Task<decimal> WithdrawAsync(Guid accountId, decimal amount, Guid sessionId)
    {
        (Session _, Account account) = await ValidateAccountAccessAsync(accountId, sessionId);

        account.Withdraw(amount);
        await _accountRepository.UpdateAsync(account);

        var transaction = new Transaction(accountId, TransactionType.Withdrawal, amount, account.Balance);
        await _transactionRepository.AddAsync(transaction);

        return account.Balance;
    }

    public async Task<decimal> DepositAsync(Guid accountId, decimal amount, Guid sessionId)
    {
        (Session _, Account account) = await ValidateAccountAccessAsync(accountId, sessionId);

        account.Deposit(amount);
        await _accountRepository.UpdateAsync(account);

        var transaction = new Transaction(accountId, TransactionType.Deposit, amount, account.Balance);
        await _transactionRepository.AddAsync(transaction);

        return account.Balance;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(Guid accountId, Guid sessionId)
    {
        (Session _, Account _) = await ValidateAccountAccessAsync(accountId, sessionId);
        return await _transactionRepository.GetByAccountIdAsync(accountId);
    }

    private async Task<Session> ValidateSessionAsync(Guid sessionId)
    {
        Session? session = await _sessionRepository.GetByIdAsync(sessionId);
        if (session == null || !session.IsValid())
            throw new UnauthorizedAccessException("Invalid or expired session");

        return session;
    }

    private async Task<(Session Session, Account Account)> ValidateAccountAccessAsync(Guid accountId, Guid sessionId)
    {
        Session session = await ValidateSessionAsync(sessionId);

        Account? account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new ArgumentException("Account not found");

        if (!session.CanAccessAccount(accountId))
            throw new UnauthorizedAccessException("Access denied");

        return (session, account);
    }
}