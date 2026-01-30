using Lab5.Applications.Contracts.Sessions.Models;
using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Mapping;

public static class SessionMappingExtensions
{
    public static SessionDto MapToDto(this UserSession session)
    {
        SessionTypeDto typeDto = session.Type.Equals(SessionType.User)
            ? SessionTypeDto.User
            : SessionTypeDto.Admin;

        return new SessionDto(session.Key.Value, typeDto, session.AccountNumber?.Value);
    }
}