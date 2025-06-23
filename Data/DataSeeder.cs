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
                new Currency { Id = 1, Name = "TRY", Symbol = '₺', TryIndexedValue = 1.0m, ISO4217Code = 949 },
                new Currency { Id = 2, Name = "USD", Symbol = '$', TryIndexedValue = 35.0m , ISO4217Code = 840},
                new Currency { Id = 3, Name = "EUR", Symbol = '€', TryIndexedValue = 40.0m, ISO4217Code =  978}
            };

            await context.Currencies.AddRangeAsync(currencies);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new User { Id = 1, Name = "Ahmet", Surname = "Yılmaz", Username = "ahmetyilmaz", Password = "12345678", Email = "ahmet.yilmaz@example.com", TotalBalanceInTRY = 1175.00m},
                new User { Id = 2, Name = "Ayşe", Surname = "Demir", Username = "aysedemir", Password = "12345678", Email = "ayse.demir@example.com", TotalBalanceInTRY = 800.00m},
                new User { Id = 3, Name = "John", Surname = "Doe", Username = "johndoe", Password = "12345678", Email = "john.doe@example.com", TotalBalanceInTRY = 1400.00m},
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if(!await context.BankAccounts.AnyAsync())
        {
            var accounts = new List<BankAccount>
            {
                new BankAccount { Id = 9491020, Balance = 900.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 1},
                new BankAccount { Id = 8405768, Balance = 3.00m, AccountType = "SavingsAccount", CurrencyId = 2, UserId = 1},
                new BankAccount { Id = 9494859, Balance = 400.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 2},
                new BankAccount { Id = 9781526, Balance = 7.00m, AccountType = "CheckingAccount", CurrencyId = 3, UserId = 2},
                new BankAccount { Id = 9499584, Balance = 450.00m, AccountType = "CheckingAccount", CurrencyId = 1, UserId = 3},
                new BankAccount { Id = 8403265, Balance = 24.00m, AccountType = "CheckingAccount", CurrencyId = 2, UserId = 3},
            };

            await context.BankAccounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
        }

        if (!await context.Transactions.AnyAsync())
        {
            var transactions = new List<Transaction>
            {
                new Transaction { Id = 1, SourceType = "Account", SourceId = 9491020, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = null, TargetId = null, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 250.00m, UserId = 1, UserName = "Ahmet", UserSurname = "Yılmaz", Type = "Deposit", CurrentBalance = 250, Timestamp = DateTime.Now.AddDays(-4)},
                new Transaction { Id = 2, SourceType = "Account", SourceId = 9491020, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = null, TargetId = null, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 750.00m, UserId = 1, UserName = "Ahmet", UserSurname = "Yılmaz", Type = "Deposit", CurrentBalance = 1000.00m, Timestamp = DateTime.Now.AddDays(-3)},
                new Transaction { Id = 3, SourceType = "Account", SourceId = 8405768, SourceCurrency = "USD", SourceCurrencySymbol = '$', TargetType = null, TargetId = null, TargetCurrency = "USD", TargetCurrencySymbol = '$', Amount = 5.00m, UserId = 1, UserName = "Ahmet", UserSurname = "Yılmaz", Type = "Deposit", CurrentBalance = 1175.00m, Timestamp = DateTime.Now.AddDays(-2)},
                new Transaction { Id = 4, SourceType = "Account", SourceId = 8405768, SourceCurrency = "USD", SourceCurrencySymbol = '$', TargetType = "Account", TargetId = 9491020, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 2.00m, UserId = 1, UserName = "Ahmet", UserSurname = "Yılmaz", Type = "Money Transfer", CurrentBalance = 1175.00m, Timestamp = DateTime.Now.AddDays(-1)},
                new Transaction { Id = 5, SourceType = "Account", SourceId = 9491020, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = "VirtualCard", TargetId = 1, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 170.00m, UserId = 1, UserName = "Ahmet", UserSurname = "Yılmaz", Type = "Virtual Card Money Transfer", CurrentBalance = 1175.00m, Timestamp = DateTime.Now.AddDays(-1)},

                new Transaction { Id = 6, SourceType = "Account", SourceId = 9494859, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = null, TargetId = null, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 500.00m, UserId = 2, UserName = "Ayşe", UserSurname = "Demir", Type = "Deposit", CurrentBalance = 500.00m, Timestamp = DateTime.Now.AddDays(-5)},
                new Transaction { Id = 7, SourceType = "Account", SourceId = 9781526, SourceCurrency = "EUR", SourceCurrencySymbol = '€', TargetType = null, TargetId = null, TargetCurrency = "EUR", TargetCurrencySymbol = '€', Amount = 10.00m, UserId = 2, UserName = "Ayşe", UserSurname = "Demir", Type = "Deposit", CurrentBalance = 900.00m, Timestamp = DateTime.Now.AddDays(-4)},
                new Transaction { Id = 8, SourceType = "Account", SourceId = 9781526, SourceCurrency = "EUR", SourceCurrencySymbol = '€', TargetType = "Account", TargetId = 9494859, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 3.00m, UserId = 2, UserName = "Ayşe", UserSurname = "Demir", Type = "Money Transfer", CurrentBalance = 900.00m, Timestamp = DateTime.Now.AddDays(-3)},
                new Transaction { Id = 9, SourceType = "Account", SourceId = 9494859, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = "VirtualCard", TargetId = 2, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 120.00m, UserId = 2, UserName = "Ayşe", UserSurname = "Demir", Type = "Virtual Card Money Transfer", CurrentBalance = 900.00m, Timestamp = DateTime.Now.AddDays(-2)},
                new Transaction { Id = 10, SourceType = "Account", SourceId = 9494859, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = null, TargetId = null, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 100.00m, UserId = 2, UserName = "Ayşe", UserSurname = "Demir", Type = "Withdraw", CurrentBalance = 800.00m, Timestamp = DateTime.Now.AddDays(-1)},

                new Transaction { Id = 11, SourceType = "Account", SourceId = 9499584, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = null, TargetId = null, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 700.00m, UserId = 3, UserName = "John", UserSurname = "Doe", Type = "Deposit", CurrentBalance = 700.00m, Timestamp = DateTime.Now.AddDays(-4)},
                new Transaction { Id = 12, SourceType = "Account", SourceId = 8403265, SourceCurrency = "USD", SourceCurrencySymbol = '$', TargetType = null, TargetId = null, TargetCurrency = "USD", TargetCurrencySymbol = '$', Amount = 20.00m, UserId = 3, UserName = "John", UserSurname = "Doe", Type = "Deposit", CurrentBalance = 1400.00m, Timestamp = DateTime.Now.AddDays(-3)},
                new Transaction { Id = 13, SourceType = "Account", SourceId = 9499584, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = "Account", TargetId = 8403265, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 140.00m, UserId = 3, UserName = "John", UserSurname = "Doe", Type = "Money Transfer", CurrentBalance = 1400.00m, Timestamp = DateTime.Now.AddDays(-2)},
                new Transaction { Id = 14, SourceType = "Account", SourceId = 9499584, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = "VirtualCard", TargetId = 4, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 60.00m, UserId = 3, UserName = "John", UserSurname = "Doe", Type = "Virtual Card Money Transfer", CurrentBalance = 1400.00m, Timestamp = DateTime.Now.AddDays(-1)},
                new Transaction { Id = 15, SourceType = "Account", SourceId = 9499584, SourceCurrency = "TRY", SourceCurrencySymbol = '₺', TargetType = "VirtualCard", TargetId = 4, TargetCurrency = "TRY", TargetCurrencySymbol = '₺', Amount = 50.00m, UserId = 3, UserName = "John", UserSurname = "Doe", Type = "Virtual Card Money Transfer", CurrentBalance = 1400.00m, Timestamp = DateTime.Now.AddDays(-1)}
            };

            await context.Transactions.AddRangeAsync(transactions);
            await context.SaveChangesAsync();
        }

        if (!await context.DebitCards.AnyAsync())
        {
            var debitCards = new List<DebitCard>
            {
                new DebitCard { Id = 1, Password = "1234", LinkedAccountId = 9491020, UserId = 1, Type = "Debit Card", CardNumber = "7257 0420 7279 0573", ExpirationDate = DateTime.Now.AddYears(10), CVV = "475", CardholderNameAndSurname = "Ahmet Yılmaz", OnlineShopping = true },
                new DebitCard { Id = 2, Password = "1234", LinkedAccountId = 9494859, UserId = 2, Type = "Debit Card", CardNumber = "9261 0866 7060 7349", ExpirationDate = DateTime.Now.AddYears(10), CVV = "526", CardholderNameAndSurname = "Ayşe Demir", OnlineShopping = true }
            };

            await context.DebitCards.AddRangeAsync(debitCards);
            await context.SaveChangesAsync();
        }

        if(!await context.VirtualCards.AnyAsync())
        {
            var virtualCards = new List<VirtualCard>
            {
                new VirtualCard { Id = 1, UserId = 1, Type = "Virtual Card", CardNumber = "2015 6008 5077 5919", ExpirationDate = DateTime.Now.AddYears(10), CVV = "151", CardholderNameAndSurname = "Ahmet Yılmaz", OnlineShopping = true, AvailableLimit = 170.00m },
                new VirtualCard { Id = 2, UserId = 2, Type = "Virtual Card", CardNumber = "2544 8717 0354 9422", ExpirationDate = DateTime.Now.AddYears(10), CVV = "971", CardholderNameAndSurname = "Ayşe Demir", OnlineShopping = true, AvailableLimit = 120.00m },
                new VirtualCard { Id = 3, UserId = 3, Type = "Virtual Card", CardNumber = "4735 7138 2671 1004", ExpirationDate = DateTime.Now.AddYears(10), CVV = "430", CardholderNameAndSurname = "John Doe", OnlineShopping = true, AvailableLimit = 60.00m },
                new VirtualCard { Id = 4, UserId = 3, Type = "Virtual Card", CardNumber = "2069 8525 7192 6469", ExpirationDate = DateTime.Now.AddYears(10), CVV = "769", CardholderNameAndSurname = "John Doe", OnlineShopping = true, AvailableLimit = 50.00m }
            };

            await context.VirtualCards.AddRangeAsync(virtualCards);
            await context.SaveChangesAsync();
        }
    }
}