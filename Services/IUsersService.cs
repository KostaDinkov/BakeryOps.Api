using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface IUsersService
    {
        Task<UserDTO[]> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid userId);
        Task<UserDTO> GetUserByNameAsync(string name);
        Task<UserDTO> CreateUserAsync(NewUserDTO newUser);
        Task<UserDTO> UpdateUserAsync(NewUserDTO user);
        Task DeleteUserAsync(Guid userId);

        Task UpdatePermissionsAsync(Guid id, string[] permissions);
    }
}
