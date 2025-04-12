using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Authentication.Core.Entities.Identities;
using BCrypt.Net;

namespace Authentication.Core.Mappers;

public static class UsersMapper
{
    public static UserDto ToUserDto(this ApplicationUser user)
    {
        return new UserDto
        {
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            //Roles = user.Roles
        };
    }
    public static SessionDto ToSessionDto(this ApplicationUser user)
    {
        return new SessionDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            //Roles = user.Roles
        };
    }
    public static ApplicationUser FromCreateUser(this UserCreateDto createUserDto)
    {
        return new ApplicationUser
        {
            FullName = createUserDto.Username,
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            //Roles = user.Roles
        };
    }
    //public static User FromUpdateUserDto(this UpdateUserDto updateUserDto, int id)
    //{
    //    return new User
    //    {
    //        Id = id,
    //        FullName = updateUserDto.FullName,
    //        Username = updateUserDto.Username,
    //        Email = updateUserDto.Email,
    //        Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password),
    //        Status = updateUserDto.Status,
    //    };
    //}
}