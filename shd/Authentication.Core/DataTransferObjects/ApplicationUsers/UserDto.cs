using System.ComponentModel.DataAnnotations;

namespace Authentication.Core.DataTransferObjects.ApplicationUsers;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Input your user name.")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "Input your user eMail.")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Input your user password.")]
    public required string Password { get; set; }
    [Required(ErrorMessage = "Confirm your user password.")]
    public string ConfirmsPassword { get; set; } = string.Empty;
    //public List<> Role { get; set; }
}
