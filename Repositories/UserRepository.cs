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

    public async Task<IEnumerable<User>> GetAllUsersAsync()=>
        await _context.Users
            .Include(user => user.BankAccounts)
            .ToListAsync();

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {   
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<User?> GetUserWithPasswordByIdAsync(int id) =>
        await _context.Users
            .Where(user => user.Id == id)
            .FirstOrDefaultAsync();
}