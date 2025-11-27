using System.ComponentModel.DataAnnotations;

namespace ERP.UI.Blazor.Models;

public class UserRegistrationDto
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
