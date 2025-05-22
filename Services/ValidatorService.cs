using Simple_Bank_Application.Data;

namespace Simple_Bank_Application.Services;

public class ValidatorService
{
    private readonly AppDbContext _context;

    public ValidatorService(AppDbContext context) => _context = context;

    public bool IsEmailValid(string email)
    {
        bool exists = _context.Users.Any(user => user.Email.ToLower().Contains(email.ToLower()));
        bool empty = string.IsNullOrEmpty(email);
        bool correctFormat = email.Contains('@');


        return !exists && !empty && correctFormat;
    }

    public bool IsUsernameValid(string username)
    {
        bool exists = _context.Users.Any(user => user.Username.ToLower().Contains(username.ToLower()));
        return !exists;
    }

    public bool IsPasswordValid(string password)
    {
        bool correctFormat = password.Length >= 8;
        return correctFormat;
    }

}
