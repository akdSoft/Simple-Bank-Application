using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models;

public class Login
{
}

public class UserLoginRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class UserLoginResponse
{
    public bool AuthenticateResult { get; set; }
    public string AuthToken { get; set; } = null!;
    public DateTime AccessTokenExpireDate { get; set; }
}