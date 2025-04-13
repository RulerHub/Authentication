using Authentication.Core.DataTransferObjects.ApplicationUsers;
using Authentication.Core.DataTransferObjects.Authorize;
using Authentication.Core.DataTransferObjects.Responses;
using Authentication.Data.Services.Users.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IUserService uService) : ControllerBase
{
    private readonly IUserService _uService = uService;

    //[HttpGet("get/{role:alpha}/{search:alpha}")]
    //public async Task<IActionResult> Index(string role, string search = "EMPTY")
    [HttpGet]
    [Route("index")]
    public async Task<IActionResult> Index()
    {
        var response = new ResponseDto<List<UserDto>>();
        try
        {
            //if (search == "EMPTY")
            //{
            //    search = "";
            //}
            var result = await _uService.GetUsersAsync();
            var count = result.Count;

            response.IsSuccessful = true;
            response.Result = result;
            response.Message = $"existen {count} usuarios en la db";
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("get/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = new ResponseDto<UserDto>();
        try
        {
            response.IsSuccessful = true;
            response.Result = await _uService.GetUserById(id);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody]UserDto dto)
    {
        var response = new ResponseDto<UserDto>();
        try
        {
            response.IsSuccessful = true;
            response.Result = await _uService.CreateUserAsync(dto);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UserDto dto)
    {
        var response = new ResponseDto<bool>();
        try
        {
            response.IsSuccessful = true;
            response.Result = await _uService.EditUserAsync(dto);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> Authorize([FromBody] UserLoginDto dto)
    {
        var response = new ResponseDto<SessionDto>();
        try
        {
            response.IsSuccessful = true;
            response.Result = await _uService.AuthorizedUser(dto);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }

    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = new ResponseDto<bool>();
        try
        {
            response.IsSuccessful = true;
            response.Result = await _uService.DeleteUserAsync(id);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            Console.WriteLine(ex.Message);
            throw;
        }
        return Ok(response);
    }
}
