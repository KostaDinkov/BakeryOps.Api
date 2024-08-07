using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOps.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController(IUsersService usersService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await usersService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await usersService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserCredentialsDTO credentials)
    {
        var user = await usersService.CreateUserAsync(credentials.UserName, credentials.Password);
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser( UserDTO update)
    {
        try
        {
            var user = await usersService.UpdateUserAsync(update);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (DbServiceException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await usersService.DeleteUserAsync(id);
        }
        catch (DbServiceException e)
        {
            return BadRequest(e.Message);
        }
        
        
        return Ok();
    }
}