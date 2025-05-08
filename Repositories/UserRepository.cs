using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

    public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);


    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var updatedUser = await _context.Users.FindAsync(user.Id);

        if (updatedUser is null) return null;

        updatedUser.Name = user.Name;
        updatedUser.Surname = user.Surname;
        updatedUser.Username = user.Username;
        updatedUser.Email = user.Email;

        _context.Users.Update(updatedUser);
        await _context.SaveChangesAsync();

        return updatedUser;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var deleted = await _context.Users.FindAsync(id);
        if (deleted is null) return false;

        _context.Users.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }
}
