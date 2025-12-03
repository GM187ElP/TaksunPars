using System.ComponentModel.DataAnnotations;

namespace ERP.UI.Blazor.Models;

public class UserLoginDto
{
    [Required(ErrorMessage = "کد ملی را وارد کنید")]
    public string Username { get; set; }
    [Required(ErrorMessage = "رمز عبور را وارد کنید")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
