using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserAsync(int id);
    }
}
