using Authentication.Core.Entities.Identities;
using Authentication.Data.Repositories.Generic;

namespace Authentication.Data.Repositories.Users.Interfaces;

public interface IUserRepository : IGenericRepository<ApplicationUser>
{
}
