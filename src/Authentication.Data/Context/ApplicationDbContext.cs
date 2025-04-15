using Authentication.Core.Entities.Identities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
