using System.ComponentModel.DataAnnotations;

namespace TDL.UI.Models
{
  public class LoginModel
  {
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6")]
    public string Password { get; set; } = string.Empty;
  }
}
