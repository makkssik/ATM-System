using Lab5.Applications.Abstractions.Repositories;
using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Infrastructure.Persistence.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly Dictionary<Guid, UserSession> _sessions = new();

    public UserSession? GetByKey(SessionKey key)
    {
        if (_sessions.ContainsKey(key.Value))
            return _sessions[key.Value];

        return null;
    }

    public void Save(UserSession session)
    {
        _sessions[session.Key.Value] = session;
    }
}