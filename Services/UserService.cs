using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Exceptions;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BakeryOps.API.Services
{
    public class UserService(AppDb dbContext, IMapper mapper, ILogger<OrdersService> logger) : IUsersService
    {
        public async Task<UserDTO[]> GetUsersAsync()
        {
            var users = await dbContext.Users.Include(u=>u.Permissions).ToArrayAsync();
            return users.Select(u => mapper.Map<User, UserDTO>(u)).ToArray();
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await dbContext.Users.Include(u=>u.Permissions).FirstOrDefaultAsync(u=>u.Id==userId);
            if (user == null)
            {
                throw new DbServiceException($"User {userId} not found");
            }
            return mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByNameAsync(string username)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return mapper.Map<User, UserDTO>(user);
        }

        public async Task<UserDTO> CreateUserAsync(NewUserDTO newUser)
        {
            var user = new User
            {
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName

            };
            user.HashedPassword = new PasswordHasher<User>().HashPassword(user, newUser.Password);
            if (dbContext.Users.Any(u => u.UserName == newUser.UserName))
            {
                throw new DbServiceException($"User {newUser.UserName} already exists");
            }
            foreach (var permission in newUser.Permissions)
            {
                user.Permissions.Add(new Permission(permission));
            }

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var result = mapper.Map<User, UserDTO>(user);
            return await Task.FromResult(result);
        }

        

        public async Task<UserDTO> UpdateUserAsync(NewUserDTO updated)
        {
            var id = Guid.Parse(updated.Id);
            var user = await dbContext.Users.FindAsync(id);
            if (user is null)
            {
                throw new DbServiceException($"User {id} not found");
            }

            user.UserName = updated.UserName;
            
            
            var userPermissions = dbContext.Permissions.Where(p => p.UserId == id);
            dbContext.Permissions.RemoveRange(userPermissions);
            
            user.Permissions.Clear();
            foreach (var permission in updated.Permissions)
            {
                user.Permissions.Add(new Permission(permission));
            }

            user.FirstName = updated.FirstName;
            user.LastName = updated.LastName;
            //if password is not null, update password
            if (!string.IsNullOrEmpty(updated.Password))
            {
                user.HashedPassword = new PasswordHasher<User>().HashPassword(user, updated.Password);
            }

            await dbContext.SaveChangesAsync();

            return await Task.FromResult(updated);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            
            var user = await dbContext.Users.FindAsync(userId);
            if (user != null) dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }


        public async Task UpdatePermissionsAsync(Guid userId, string[] permission)
        {
            var user = await dbContext.Users.FindAsync(userId);
            user.Permissions.Clear();
            foreach (var perm in permission)
            {
                user.Permissions.Add(new Permission(perm));
            }
            await dbContext.SaveChangesAsync();

        }
    }
}