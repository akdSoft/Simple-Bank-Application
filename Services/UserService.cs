using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly ICurrencyService _currencyService;
    private readonly IBankAccountService _bankAccountService;

    public UserService(IUserRepository repo, ICurrencyService currencyService, IBankAccountService bankAccountService)
    {
        _userRepo = repo;
        _currencyService = currencyService;
        _bankAccountService = bankAccountService;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepo.GetAllUsersAsync();
        return DtoMapper.ToDtoList(users);
    }

    public async Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id)
    {
        var updatedUser = await _userRepo.GetUserWithPasswordByIdAsync(id);

        if (updatedUser == null) return null;

        updatedUser.Name = dto.Name;
        updatedUser.Surname = dto.Surname;
        updatedUser.Username = dto.Username;
        updatedUser.Password = dto.Password;
        updatedUser.Email = dto.Email;

        await _userRepo.UpdateUserAsync(updatedUser);
        return DtoMapper.ToDto(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var userToDelete = await _userRepo.GetUserWithPasswordByIdAsync(id);
        if (userToDelete == null) return false;

        await _userRepo.DeleteUserAsync(userToDelete);
        return true;
    }

    public async Task<User?> GetUserByUsernameAsync(string username) => await _userRepo.GetUserByUsernameAsync(username);

    public async Task<User?> GetUserWithPasswordByIdAsync(int id) => await _userRepo.GetUserWithPasswordByIdAsync(id);

    public async Task<UserDto?> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
        };

        await _userRepo.CreateUserAsync(user);
        return DtoMapper.ToDto(user);
    }

    public async Task<UserDto?> IncreaseOrDecreaseTotalBalanceAsync(int accountId, decimal amount)
    {
        var account = await _bankAccountService.GetBankAccountByIdAsync(accountId);
        if (account == null) return null;

        var _amount = await _currencyService.ConvertCurrencyAsync(amount, account.Currency.Name, "TRY");

        var user = await _userRepo.GetUserWithPasswordByIdAsync(account.UserId);
        if (user == null) return null;

        user.TotalBalanceInTRY += _amount.Value;
        await _userRepo.UpdateUserAsync(user);

        return DtoMapper.ToDto(user);
    }
}
