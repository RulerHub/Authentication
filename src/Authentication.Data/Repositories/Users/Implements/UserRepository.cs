using Authentication.Core.Entities.Identities;
using Authentication.Data.Context;
using Authentication.Data.Repositories.Generic;
using Authentication.Data.Repositories.Users.Interfaces;

namespace Authentication.Data.Repositories.Users.Implements;

public class UserRepository(ApplicationDbContext context) : GenericRepository<ApplicationUser>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;
}
