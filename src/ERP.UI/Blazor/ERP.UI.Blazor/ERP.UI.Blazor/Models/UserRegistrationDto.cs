using System.ComponentModel.DataAnnotations;

namespace ERP.UI.Blazor.Models;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "کد ملی را وارد کنید")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید 10 رقم باشد")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "رمز عبور را وارد کنید")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید")]
    [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نمی باشد")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}
