namespace Simple_Bank_Application.Models;

public class Token
{
}

public class GenerateTokenRequest
{
    public string Username { get; set; } = null!;
}

public class GenerateTokenResponse
{
    public string Token { get; set; } = null!;
    public DateTime TokenExpireDate { get; set; }
}
