using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> RegisterUserAsync(CreateUserDto dto);
    Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id);
    Task<bool> DeleteUserAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
}
