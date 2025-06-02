using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(user => user.BankAccounts)
            .Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                BankAccounts = user.BankAccounts
                    .Select(acc => new BankAccountDto
                    {
                        Id = acc.Id,
                        Balance = acc.Balance,
                        AccountType = acc.AccountType,
                        UserId = acc.UserId,
                        UserName = acc.User.Name,
                        UserSurname = acc.User.Surname
                    })
                    .ToList()
            })
            .ToListAsync();
    }
    



    public async Task<UserDto?> RegisterUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();



        var createdUser = await _context.Users
            .Include(u => u.BankAccounts)
            .Where(u => u.Id == user.Id)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Username = u.Username,
                Email = u.Email,
                BankAccounts = u.BankAccounts.Select(acc => new BankAccountDto
                {
                    Id = acc.Id,
                    Balance = acc.Balance,
                    AccountType = acc.AccountType,
                    UserId = acc.UserId,
                    UserName = acc.User.Name,
                    UserSurname = acc.User.Surname
                }).ToList()
            }).FirstOrDefaultAsync();

        return createdUser;
    }

    public async Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id)
    {
        var updatedUser = await _context.Users.FindAsync(id);

        if (updatedUser is null) return null;

        updatedUser.Name = dto.Name;
        updatedUser.Surname = dto.Surname;
        updatedUser.Username = dto.Username;
        updatedUser.Password = dto.Password;
        updatedUser.Email = dto.Email;

        _context.Users.Update(updatedUser);
        await _context.SaveChangesAsync();

        var updatedUserDto = await _context.Users
            .Include(user => user.BankAccounts)
            .Where(user => user.Id == id)
            .Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                BankAccounts = user.BankAccounts.Select(acc => new BankAccountDto
                {
                    Id = acc.Id,
                    Balance = acc.Balance,
                    AccountType = acc.AccountType,
                    UserId = acc.UserId,
                    UserName = acc.User.Name,
                    UserSurname = acc.User.Surname
                }).ToList()
            }).FirstOrDefaultAsync();

        return updatedUserDto;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var deleted = await _context.Users.FindAsync(id);
        if (deleted is null) return false;

        _context.Users.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserWithPasswordByIdAsync(int id)
    {
        return await _context.Users
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();
    }
}
