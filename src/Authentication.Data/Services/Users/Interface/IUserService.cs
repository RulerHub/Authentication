using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;

namespace Authentication.Data.Services.Users.Interface;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserById(int Id);
    Task<SessionDto> AuthorizedUser(UserLoginDto login);
    Task<UserDto> CreateUserAsync(UserCreateDto user);
    Task<bool> EditUserAsync(UserUpdateDto user);
    Task<bool> DeleteUserAsync(int id);
}
