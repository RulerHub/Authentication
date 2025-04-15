using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Microsoft.AspNetCore.Http;

namespace Authentication.Data.Services.Users.Interface;

/// <summary>
/// Interfaz que define las operaciones relacionadas con la gestión de usuarios en el sistema.
/// Implementa métodos para realizar acciones como obtener usuarios, crear, editar, eliminar, 
/// y autorizar usuarios con soporte para cookies de autenticación.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Obtiene una lista de todos los usuarios registrados en el sistema.
    /// </summary>
    /// <returns>Una lista de objetos <see cref="UserDto"/> que representan los usuarios.</returns>
    Task<List<UserDto>> GetUsersAsync();

    /// <summary>
    /// Obtiene los datos de un usuario específico basado en su ID.
    /// </summary>
    /// <param name="id">El identificador único del usuario.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario solicitado.</returns>
    Task<UserDto> GetUserById(int id);

    /// <summary>
    /// Crea un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="user">Un objeto <see cref="UserDto"/> que contiene los datos del usuario a crear.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario creado.</returns>
    Task<UserDto> CreateUserAsync(UserDto user);

    /// <summary>
    /// Actualiza los datos de un usuario existente en el sistema.
    /// </summary>
    /// <param name="user">Un objeto <see cref="UserDto"/> que contiene los datos actualizados del usuario.</param>
    /// <returns>Un valor booleano que indica si la actualización fue exitosa.</returns>
    Task<bool> EditUserAsync(UserDto user);

    /// <summary>
    /// Elimina un usuario del sistema basado en su ID.
    /// </summary>
    /// <param name="id">El identificador único del usuario a eliminar.</param>
    /// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
    Task<bool> DeleteUserAsync(int id);
    /// <summary>
    /// Cierra la sesión del usuario actual y elimina la cookie de autenticación.
    /// </summary>
    /// <param name="httpContext">El contexto HTTP actual para manejar la autenticación.</param>
    /// <returns>Una tarea que representa la operación asincrónica.</returns>
    Task LogoutAsync(HttpContext httpContext);
    /// <summary>
    /// Autoriza a un usuario basado en sus credenciales y, opcionalmente, crea una cookie de autenticación.
    /// </summary>
    /// <param name="login">Un objeto <see cref="UserLoginDto"/> que contiene las credenciales del usuario.</param>
    /// <param name="httpContext">El contexto HTTP actual para manejar la autenticación (opcional).</param>
    /// <returns>Un objeto <see cref="SessionDto"/> con los datos de la sesión si la autenticación es exitosa.</returns>
    Task<SessionDto> AuthorizeUserAsync(UserLoginDto login, HttpContext? httpContext = null);

}
