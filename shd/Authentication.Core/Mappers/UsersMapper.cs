using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Authentication.Core.Entities.Identities;

namespace Authentication.Core.Mappers;

public static class UsersMapper
{
    public static UserDto ToUserDto(this ApplicationUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            //Roles = user.Roles
        };
    }

    public static ApplicationUser FromUserDto(this UserDto user)
    {
        return new ApplicationUser
        {
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
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