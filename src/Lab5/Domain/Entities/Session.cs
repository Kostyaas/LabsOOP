namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }

    public Guid? AccountId { get; private set; }

    public bool IsAdmin { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public Session(Guid? accountId, bool isAdmin, int durationMinutes = 30)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        IsAdmin = isAdmin;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = CreatedAt.AddMinutes(durationMinutes);
    }

    public bool IsValid() => DateTime.UtcNow < ExpiresAt;

    public bool CanAccessAccount(Guid accountId) => IsAdmin || (AccountId.HasValue && AccountId.Value == accountId);
}