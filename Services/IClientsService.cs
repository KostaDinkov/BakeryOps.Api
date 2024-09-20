using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface IClientsService
    {

        Task<Client> GetClientById(int id);
        Task<List<ClientDTO>> GetAllClients();
        Task<ClientDTO?> AddClient(ClientDTO client);
        Task<bool> DeleteClient(int id);
        
        
        Task<ClientDTO?> UpdateClient(ClientDTO update);
    }
}
