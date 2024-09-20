using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class ClientsService(AppDb dbContext, IMapper mapper, ILogger<ClientsService> logger)
        : IClientsService
    {
        private ILogger logger = logger;

        public async Task<ClientDTO?> AddClient(ClientDTO newClient)
        {
            var client = mapper.Map<Client>(newClient);
            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            
            return mapper.Map<ClientDTO>(client);
        }

        public async Task<bool> DeleteClient(int id)
        {
            var client = await dbContext.Clients.Include(c => c.Orders).Where(c => c.IsDeleted != true)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (client is null)
            {
                return false;
            }

            if (client.Orders.Any())
            {
                client.IsDeleted = true;
            }
            else
            {
                dbContext.Clients.Remove(client);
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ClientDTO>> GetAllClients()
        {
            var clients = await dbContext.Clients.Where(c=>c.IsDeleted!= true).ToListAsync();
            var clientDtos = clients.Select( mapper.Map<ClientDTO>).ToList();
            
            return clientDtos;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await dbContext.Clients.FindAsync(id);

            return client;
        }

        public async Task<ClientDTO?> UpdateClient(ClientDTO update)
        {
            var client = await dbContext.Clients.FindAsync(update.Id);
            if (client is null)
            {
                return null;
            }

            mapper.Map(update, client);
            await dbContext.SaveChangesAsync();
            return update;
        }
    }
}