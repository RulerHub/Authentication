using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Core.DataTransferObjects.ApplicationUsers;

public class UserDto
{
    [MaxLength(50)]
    public string FullName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string Username { get; set; } = String.Empty;
    [MaxLength(50)]
    public string Email { get; set; } = String.Empty;
    [MaxLength(100)]
    public string Password { get; set; } = String.Empty; // Stores hashed password
    //public List<> Roles { get; set; } = String.Empty;
}
