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

public class UserService(IUserRepository uRepository) : IUserService
{
    private readonly IUserRepository _uRepository = uRepository;

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

    public async Task<UserDto> CreateUserAsync(UserDto user)
    {
        try
        {
            var model = user.FromUserDto();
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
