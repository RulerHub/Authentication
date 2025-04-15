using System.ComponentModel.DataAnnotations;

namespace Authentication.Core.DataTransferObjects.Authorize;

public class UserLoginDto
{
    [Required(ErrorMessage = "Input your user eMail.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Input your user password.")]
    public required string Password { get; set; }
}
