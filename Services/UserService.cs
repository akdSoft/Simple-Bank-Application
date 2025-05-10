using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync() => await _repo.GetAllUsersAsync();

    public async Task<UserDto?> GetUserByIdAsync(int id) => await _repo.GetUserByIdAsync(id);

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto) => await _repo.RegisterUserAsync(dto);

    public async Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id) => await _repo.UpdateUserAsync(dto, id);


    public async Task<bool> DeleteUserAsync(int id) => await _repo.DeleteUserAsync(id);
}
