using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;

namespace BakeryOps.API.Services
{
    public interface IClientsService
    {

        Task<Client> GetClientById(int id);
        Task<List<ClientDTO>> GetAllClients();
        Task<Client> AddClient(ClientDTO client);
        Task<Client> DeleteClient(int id);
        
        
        Task<Client> UpdateClient(int id, ClientDTO update);
    }
}
