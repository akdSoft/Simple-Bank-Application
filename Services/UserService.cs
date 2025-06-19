using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository repo) => _userRepo = repo;

    public async Task<User?> Authenticate(string username, string password)
    {
        if (username == "admin" && password == "admin")
            return new User { Username = "admin", Password = "admin" };

        var user = await _userRepo.GetUserByUsernameAsync(username);
        if (user == null || password != user.Password) return null;
        return user;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepo.GetAllUsersAsync();
        return DtoMapper.ToDtoList(users);
    }

    public async Task<UserDto?> UpdateUserByDtoAsync(CreateUserDto dto, int id)
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
    public async Task<UserDto?> UpdateUserAsync(User user)
    {
        var updatedUser = await _userRepo.GetUserWithPasswordByIdAsync(user.Id);
        if (updatedUser == null) return null;

        updatedUser.Name = user.Name;
        updatedUser.Surname = user.Surname;
        updatedUser.Password = user.Password;
        updatedUser.Email = user.Email;
        updatedUser.TotalBalanceInTRY = user.TotalBalanceInTRY;

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
}
