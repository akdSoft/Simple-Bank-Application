using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<User?> GetUserWithPasswordByIdAsync(int id);
    Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id);
    Task<bool> DeleteUserAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
}
