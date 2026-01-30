using Lab5.Applications.Contracts.Accounts.Models;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Mapping;

public static class MoneyMappingExtensions
{
    public static MoneyDto MapToDto(this Money money)
    {
        return new MoneyDto(money.Value);
    }
}