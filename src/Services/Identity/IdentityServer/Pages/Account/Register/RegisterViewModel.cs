using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Pages.Account.Register;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Role { get; set; }
    public string ReturnUrl { get; set; }
    public string Button { get; set; }
}
