using Orders.Models.DTOs;
using Orders.Models;
using Orders.API.Models.DTOs;

namespace Orders.API.Services
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
