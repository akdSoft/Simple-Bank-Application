using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Data;

public class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!await context.Currencies.AnyAsync())
        {
            var currencies = new List<Currency>
            {
                new Currency { Id = 1, Name = "TRY", Symbol = '₺', TryIndexedValue = 1.0m },
                new Currency { Id = 2, Name = "USD", Symbol = '$', TryIndexedValue = 35.0m },
                new Currency { Id = 3, Name = "EUR", Symbol = '€', TryIndexedValue = 40.0m }
            };

            await context.Currencies.AddRangeAsync(currencies);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new User { Id = 1, Name = "Ahmet", Surname = "Yılmaz", Username = "ahmetyilmaz", Password = "123", Email = "ahmet.yilmaz@example.com", TotalBalanceInTRY = 675.00m},
                new User { Id = 2, Name = "Ayşe", Surname = "Demir", Username = "aysedemir", Password = "123", Email = "ayse.demir@example.com", TotalBalanceInTRY = 1400.00m},
                new User { Id = 3, Name = "John", Surname = "Doe", Username = "johndoe", Password = "123", Email = "john.doe@example.com", TotalBalanceInTRY = 960.00m},
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if(!await context.BankAccounts.AnyAsync())
        {
            var accounts = new List<BankAccount>
            {
                new BankAccount { Id = 1, Balance = 400.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 1},
                new BankAccount { Id = 2, Balance = 6.00m, AccountType = "SavingsAccount", CurrencyId = 2, UserId = 1},
                new BankAccount { Id = 3, Balance = 950.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 2},
                new BankAccount { Id = 4, Balance = 5.00m, AccountType = "CheckingAccount", CurrencyId = 3, UserId = 2},
                new BankAccount { Id = 5, Balance = 675.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 3},
                new BankAccount { Id = 6, Balance = 6.00m, AccountType = "CheckingAccount", CurrencyId = 2, UserId = 3},
            };

            await context.BankAccounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
        }

        if (!await context.Transactions.AnyAsync())
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, SourceType = "Account", SourceId = 1, SourceCurrency = "TRY", TargetType = null, TargetId = null, TargetCurrency = "TRY", Amount = 500.00m, UserId = 1, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 2, SourceType = "Account", SourceId = 2, SourceCurrency = "USD", TargetType = null, TargetId = null, TargetCurrency = "USD", Amount = 5.00m, UserId = 1, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 3, SourceType = "Account", SourceId = 1, SourceCurrency = "TRY", TargetType = "Account", TargetId = 2, TargetCurrency = "USD", Amount = 35.00m, UserId = 1, Type = "Money Transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 4, SourceType = "Account", SourceId = 1, SourceCurrency = "TRY", TargetType = "VirtualCard", TargetId = 1, TargetCurrency = "TRY", Amount = 65.00m, UserId = 1, Type = "virtual card money transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 5, SourceType = "Account", SourceId = 3, SourceCurrency = "TRY", TargetType = null, TargetId = null, TargetCurrency = "TRY", Amount = 1000.00m, UserId = 2, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 6, SourceType = "Account", SourceId = 4, SourceCurrency = "EUR", TargetType = null, TargetId = null, TargetCurrency = "EUR", Amount = 10.00m, UserId = 2, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 7, SourceType = "Account", SourceId = 4, SourceCurrency = "EUR", TargetType = "Account", TargetId = 3, TargetCurrency = "TRY", Amount = 5.00m, UserId = 2, Type = "Money Transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 8, SourceType = "Account", SourceId = 3, SourceCurrency = "TRY", TargetType = "VirtualCard", TargetId = 2, TargetCurrency = "TRY", Amount = 100.00m, UserId = 2, Type = "virtual card money transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 9, SourceType = "Account", SourceId = 3, SourceCurrency = "TRY", TargetType = "VirtualCard", TargetId = 2, TargetCurrency = "TRY", Amount = 150.00m, UserId = 2, Type = "virtual card money transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 10, SourceType = "Account", SourceId = 5, SourceCurrency = "TRY", TargetType = null, TargetId = null, TargetCurrency = "TRY", Amount = 750.00m, UserId = 3, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 11, SourceType = "Account", SourceId = 6, SourceCurrency = "USD", TargetType = null, TargetId = null, TargetCurrency = "USD", Amount = 6.00m, UserId = 3, Type = "Deposit", Timestamp = DateTime.Now},
                new Transaction { Id = 12, SourceType = "Account", SourceId = 5, SourceCurrency = "TRY", TargetType = "VirtualCard", TargetId = 3, TargetCurrency = "TRY", Amount = 50.00m, UserId = 3, Type = "virtual card money transfer", Timestamp = DateTime.Now},
                new Transaction { Id = 13, SourceType = "Account", SourceId = 5, SourceCurrency = "TRY", TargetType = "VirtualCard", TargetId = 4, TargetCurrency = "TRY", Amount = 25.00m, UserId = 3, Type = "virtual card money transfer", Timestamp = DateTime.Now}
            };

            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }

        if(!await context.DebitCards.AnyAsync())
        {
            var debitCards = new List<DebitCard>
            {
                new DebitCard { Id = 1, Password = "1234", LinkedAccountId = 1, UserId = 1, Type = "Debit Card", CardNumber = "7257 0420 7279 0573 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "475", CardholderNameAndSurname = "Ahmet Yılmaz", OnlineShopping = true },
                new DebitCard { Id = 2, Password = "1234", LinkedAccountId = 3, UserId = 2, Type = "Debit Card", CardNumber = "9261 0866 7060 7349 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "526", CardholderNameAndSurname = "Ayşe Demir", OnlineShopping = true }
            };

            await context.DebitCards.AddRangeAsync(debitCards);
            await context.SaveChangesAsync();
        }

        if(!await context.VirtualCards.AnyAsync())
        {
            var virtualCards = new List<VirtualCard>
            {
                new VirtualCard { Id = 1, UserId = 1, Type = "Virtual Card", CardNumber = "2015 6008 5077 5919 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "151", CardholderNameAndSurname = "Ahmet Yılmaz", OnlineShopping = true },
                new VirtualCard { Id = 2, UserId = 2, Type = "Virtual Card", CardNumber = "2544 8717 0354 9422 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "971", CardholderNameAndSurname = "Ayşe Demir", OnlineShopping = true },
                new VirtualCard { Id = 3, UserId = 3, Type = "Virtual Card", CardNumber = "4735 7138 2671 1004 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "430", CardholderNameAndSurname = "John Doe", OnlineShopping = true },
                new VirtualCard { Id = 4, UserId = 3, Type = "Virtual Card", CardNumber = "2069 8525 7192 6469 ", ExpirationDate = DateTime.Now.AddYears(10), CVV = "769", CardholderNameAndSurname = "John Doe", OnlineShopping = true }
            };

            await context.VirtualCards.AddRangeAsync(virtualCards);
            await context.SaveChangesAsync();
        }
    }
}