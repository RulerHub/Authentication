using System.Security.Claims;
using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Authentication.Core.Mappers;
using Authentication.Data.Repositories.Users.Interfaces;
using Authentication.Data.Services.Users.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data.Services.Users.Implement;

public class UserService(IUserRepository uRepository) : IUserService
{
    private readonly IUserRepository _uRepository = uRepository;
    public async Task<UserDto> CreateUserAsync(UserDto user)
    {
        try
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El objeto UserDto no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("El email y la contraseña son obligatorios.");
            }

            // Verificar si ya existe un usuario con el mismo email
            var existingUser = await _uRepository.GetAll(p => p.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new InvalidOperationException("Ya existe un usuario registrado con ese email.");
            }

            // Mapear el DTO al modelo de datos y hashear la contraseña
            var model = user.FromUserDto();
            model.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Crear el nuevo usuario
            var newUser = await _uRepository.Create(model);
            if (newUser.Id == 0)
            {
                throw new InvalidOperationException("No se pudo crear el usuario.");
            }

            return newUser.ToUserDto();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CreateUserAsync: {ex.Message}");
            throw;
        }
    }

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

    public async Task<bool> EditUserAsync(UserDto user)
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

    public async Task<UserDto> GetUserById(int id)
    {
        try
        {
            var query = _uRepository.GetAll(p => p.Id == id);
            var user = await query.FirstOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado.");
            }

            return user.ToUserDto();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetUserById: {ex.Message}");
            throw new InvalidOperationException("Ocurrió un error al obtener el usuario.");
        }
    }

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
    public async Task LogoutAsync(HttpContext httpContext)
    {
        try
        {
            // Cierra la sesión y elimina la cookie de autenticación
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task<SessionDto> AuthorizeUserAsync(UserLoginDto login, HttpContext? httpContext = null)
    {
        try
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                throw new ArgumentException("Credenciales inválidas.");
            }

            // Validar credenciales del usuario
            var query = _uRepository.GetAll(p => p.Email == login.Email);
            var user = await query.FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                // Aquí puedes incrementar un contador de intentos fallidos
                throw new UnauthorizedAccessException("Credenciales incorrectas.");
            }

            var session = user.ToSessionDto();

            // Si se proporciona un contexto HTTP, crear la cookie de autenticación
            if (httpContext != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, session.Username),
                    new(ClaimTypes.Email, session.Email),
                    new("SessionId", session.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    AllowRefresh = true
                };

                await httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }

            return session;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en AuthorizeUserAsync: {ex.Message}");
            throw new UnauthorizedAccessException("No se pudo autorizar al usuario.");
        }
    }
}
