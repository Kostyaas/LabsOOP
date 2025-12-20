using Itmo.ObjectOrientedProgramming.Lab5.Domain.Entities;

namespace Itmo.ObjectOrientedProgramming.Lab5.Domain.Ports;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(Guid id);

    Task AddAsync(Session session);
}