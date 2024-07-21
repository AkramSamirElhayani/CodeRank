using System.ComponentModel.DataAnnotations;

namespace CodeRank.App.Identity;

public class RegisterModel
{
    public string Username { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }
    public AccountType AccountType { get; set; }
}
public enum AccountType
{
    Instructor,
    Student
}
public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class RegistrationResponse
{
    public bool IsSuccessfulRegistration { get; set; }
    public IEnumerable<string> Errors { get; set; }

}

public class AuthResponse
{
    public bool IsAuthSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}


public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}