using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Mappers;

public static class DtoMapper
{
    public static BankAccountDto ToDto(BankAccount account)
    {
        return new BankAccountDto
        {
            Id = account.Id,
            Balance = account.Balance,
            AccountType = account.AccountType,
            CurrencyType = account.Currency.Name,
            CurrencySymbol = account.Currency.Symbol,
            UserId = account.UserId,
            UserName = account.User.Name,
            UserSurname = account.User.Surname
        };
    }
    public static List<BankAccountDto> ToDtoList(IEnumerable<BankAccount> accounts)
    {
        return accounts.Select(acc => ToDto(acc)).ToList();
    }
}
