using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;

namespace Authentication.Data.Services.Users.Interface;

/// <summary>
/// Servicio para gestionar las operaciones relacionadas con los usuarios.
/// </summary>
/// <remarks>
/// </remarks>
/// <param name="uRepository">Repositorio de usuarios.</param>
public interface IUserService
{
    /// <summary>
    /// Obtiene una lista de todos los usuarios.
    /// </summary>
    /// <returns>Una lista de objetos <see cref="UserDto"/>.</returns>
    Task<List<UserDto>> GetUsersAsync();
    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario.</returns>
    Task<UserDto> GetUserById(int Id);
    /// <summary>
    /// Autoriza a un usuario basado en sus credenciales.
    /// </summary>
    /// <param name="login">DTO con las credenciales del usuario.</param>
    /// <returns>Un objeto <see cref="SessionDto"/> si la autenticación es exitosa.</returns>
    Task<SessionDto> AuthorizedUser(UserLoginDto login);
    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="user">DTO con los datos del usuario a crear.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario creado.</returns>
    Task<UserDto> CreateUserAsync(UserDto user);
    /// <summary>
    /// Edita los datos de un usuario.
    /// </summary>
    /// <param name="user">DTO con los datos actualizados del usuario.</param>
    /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
    Task<bool> EditUserAsync(UserDto user);
    /// <summary>
    /// Elimina un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa, de lo contrario False.</returns>
    Task<bool> DeleteUserAsync(int id);
}
