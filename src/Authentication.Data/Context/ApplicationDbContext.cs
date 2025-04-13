using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.Entities.Identities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
