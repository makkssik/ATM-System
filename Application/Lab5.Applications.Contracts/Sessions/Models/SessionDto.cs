namespace Lab5.Applications.Contracts.Sessions.Models;

public enum SessionTypeDto
{
    User,
    Admin,
}

public sealed record SessionDto(Guid Id, SessionTypeDto Type, int? AccountNumber);