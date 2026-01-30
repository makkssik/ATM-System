using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Abstractions.Repositories;

public interface ISessionRepository
{
    UserSession? GetByKey(SessionKey key);

    void Save(UserSession session);
}