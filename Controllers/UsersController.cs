using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Security;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace BakeryOps.API.Controllers;

 

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController(ICrudService<UserDTO> usersService) : Controller
{
    private const string UsersRead = "users.read";
    private const string UsersWrite = "users.write";
    
    [HttpGet]
    [Permission(UsersRead)]
    public async Task<IActionResult> GetUsers()
    {
        var users = await usersService.GetAll();
        return Ok(users);
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUserByUsername(string userName)
    {
        var user = await (usersService as IGetByName<UserDTO>).GetByName(userName);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await usersService.GetById(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    [Permission(UsersWrite)]
    public async Task<IActionResult> AddUser(NewUserDTO newUser)
    {
        var user = await usersService.Create(newUser);
        return Ok(user);
    }

    [HttpPut]
    [Permission(UsersWrite)]
    public async Task<IActionResult> UpdateUser( NewUserDTO update)
    {
        try
        {
            var user = await usersService.Update(update);
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
    [Permission(UsersWrite)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await usersService.Delete(id);
        }
        catch (DbServiceException e)
        {
            return BadRequest(e.Message);
        }
        
        
        return Ok();
    }
}