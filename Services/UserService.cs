using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _repo.GetAllUsersAsync();
        return DtoMapper.ToDtoList(users);
    }

    public async Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id)
    {
        var updatedUser = await _repo.GetUserWithPasswordByIdAsync(id);

        if (updatedUser == null) return null;

        updatedUser.Name = dto.Name;
        updatedUser.Surname = dto.Surname;
        updatedUser.Username = dto.Username;
        updatedUser.Password = dto.Password;
        updatedUser.Email = dto.Email;

        await _repo.UpdateUserAsync(updatedUser);
        return DtoMapper.ToDto(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var userToDelete = await _repo.GetUserWithPasswordByIdAsync(id);
        if (userToDelete == null) return false;

        await _repo.DeleteUserAsync(userToDelete);
        return true;
    }

    public async Task<User?> GetUserByUsernameAsync(string username) => await _repo.GetUserByUsernameAsync(username);

    public async Task<User?> GetUserWithPasswordByIdAsync(int id) => await _repo.GetUserWithPasswordByIdAsync(id);

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

        await _repo.CreateUserAsync(user);
        return DtoMapper.ToDto(user);
    }
}
