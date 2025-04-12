using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Authentication.Core.Mappers;
using Authentication.Data.Repositories.Users.Interfaces;
using Authentication.Data.Services.Users.Interface;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Services.Users.Implement;

/// <summary>
/// Servicio para gestionar las operaciones relacionadas con los usuarios.
/// </summary>
/// <remarks>
/// Constructor de la clase UserService.
/// </remarks>
/// <param name="uRepository">Repositorio de usuarios.</param>
public class UserService(IUserRepository uRepository) : IUserService
{
    private readonly IUserRepository _uRepository = uRepository;

    /// <summary>
    /// Autoriza a un usuario basado en sus credenciales.
    /// </summary>
    /// <param name="login">DTO con las credenciales del usuario.</param>
    /// <returns>Un objeto <see cref="SessionDto"/> si la autenticación es exitosa.</returns>
    public async Task<SessionDto> AuthorizedUser(UserLoginDto login)
    {
        try
        {
            var query = _uRepository.GetAll(p => p.Email == login.Email && p.Password == login.Password);
            var user = await query.FirstOrDefaultAsync();
            if (user != null)
            {
                return user.ToSessionDto();
            }
            else
            {
                throw new TaskCanceledException("No existe un usuario con esas credenciales.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="user">DTO con los datos del usuario a crear.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario creado.</returns>
    public async Task<UserDto> CreateUserAsync(UserCreateDto user)
    {
        try
        {
            var model = user.FromCreateUser();
            var newUser = await _uRepository.Create(model);
            if (newUser.Id != 0)
            {
                return newUser.ToUserDto();
            }
            else
            {
                throw new TaskCanceledException("No se pudo crear el usuario.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Elimina un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa, de lo contrario False.</returns>
    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            var query = _uRepository.GetAll(p => p.Id == id);
            var queryUser = await query.FirstOrDefaultAsync();

            if (queryUser != null)
            {
                var response = await _uRepository.Delete(queryUser);
                if (!response)
                {
                    throw new TaskCanceledException("No se pudo eliminar el usuario.");
                }
                return response;
            }
            else
            {
                throw new TaskCanceledException("No se encontraron resultados.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Edita los datos de un usuario.
    /// </summary>
    /// <param name="user">DTO con los datos actualizados del usuario.</param>
    /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
    public async Task<bool> EditUserAsync(UserUpdateDto user)
    {
        try
        {
            var query = _uRepository.GetAll(p => p.Id == user.Id);
            var queryUser = await query.FirstOrDefaultAsync();
            if (queryUser != null)
            {
                queryUser.Username = user.Username;
                queryUser.Email = user.Email;
                queryUser.UpdateDate = DateTime.Now;

                var response = await _uRepository.Update(queryUser);
                if (!response)
                {
                    throw new TaskCanceledException("No se pudo actualizar el usuario.");
                }
                return response;
            }
            else
            {
                throw new TaskCanceledException("No se encontraron resultados.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">ID del usuario.</param>
    /// <returns>Un objeto <see cref="UserDto"/> con los datos del usuario.</returns>
    public async Task<UserDto> GetUserById(int id)
    {
        try
        {
            var query = _uRepository.GetAll(p => p.Id == id);
            var user = await query.FirstOrDefaultAsync();
            if (user != null)
            {
                return user.ToUserDto();
            }
            else
            {
                throw new TaskCanceledException("No se encontró el usuario.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Obtiene una lista de todos los usuarios.
    /// </summary>
    /// <returns>Una lista de objetos <see cref="UserDto"/>.</returns>
    public async Task<List<UserDto>> GetUsersAsync()
    {
        try
        {
            var users = await _uRepository.GetAll().ToListAsync();
            return users.Select(u => u.ToUserDto()).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
