using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _repo.GetAllUsersAsync();

    public async Task<User?> GetUserByIdAsync(int id) => await _repo.GetUserByIdAsync(id);

    public async Task<User> CreateUserAsync(User user) => await _repo.CreateUserAsync(user);

    public async Task<User?> UpdateUserAsync(User user) => await _repo.UpdateUserAsync(user);


    public async Task<bool> DeleteUserAsync(int id) => await _repo.DeleteUserAsync(id);
}
