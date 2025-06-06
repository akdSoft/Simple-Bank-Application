﻿using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync() => await _repo.GetAllUsersAsync();

    public async Task<UserDto?> UpdateUserAsync(CreateUserDto dto, int id) => await _repo.UpdateUserAsync(dto, id);

    public async Task<bool> DeleteUserAsync(int id) => await _repo.DeleteUserAsync(id);

    public async Task<User?> GetUserByUsernameAsync(string username) => await _repo.GetUserByUsernameAsync(username);

    public async Task<User?> GetUserWithPasswordByIdAsync(int id) => await _repo.GetUserWithPasswordByIdAsync(id);
}
