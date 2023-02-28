using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orders.API.Models.DTOs;
using Orders.Data;
using Orders.Models;

namespace Orders.API.Services
{
    public class ClientsService : IClientsService
    {
        private readonly OrdersDB dbContext;
        private readonly IMapper mapper;

        public ClientsService(OrdersDB dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper; 
        }

        public async Task<Client> AddClient(ClientDTO newClient)
        {
            var client = mapper.Map<Client>(newClient);
            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();
            return client;

        }

        public async Task<Client> DeleteClient(int id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client is null)
            {
                throw new InvalidOperationException();
            }
            dbContext.Remove(client);
            dbContext.SaveChanges();
            return client;
        }

        public async Task<List<ClientDTO>> GetAllClients()
        {
            var clients = await dbContext.Clients.ToListAsync();
            var clientDtos = clients.Select(c => mapper.Map<ClientDTO>(c)).ToList();
            return clientDtos;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            return client;
        }

        public async Task<Client> UpdateClient(int id, ClientDTO update)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if(client is null)
            {
                throw new InvalidOperationException();
            }
            mapper.Map(update, client);
            dbContext.SaveChanges();
            return client;
        }
    }
}
