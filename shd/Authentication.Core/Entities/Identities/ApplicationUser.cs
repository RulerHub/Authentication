using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.Entities.Abstractions;

namespace Authentication.Core.Entities.Identities;

public class ApplicationUser : Entity
{
    [MaxLength(50)]
    public required string FullName { get; set; }
    [MaxLength(50)]
    public required string Username { get; set; }
    [MaxLength(50)]
    public required string Email { get; set; }
    [MaxLength(100)]
    public required string Password { get; set; } // Stores hashed password
    [MaxLength(10)]
    public string Status { get; set; } = String.Empty;
    //public List<Role> Roles { get; set; } = [];
}
