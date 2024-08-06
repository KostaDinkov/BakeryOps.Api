using AutoMapper;
using BakeryOps.API.Data;
using BakeryOps.API.Models;
using BakeryOps.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BakeryOps.API.Services
{
    public class ClientsService : IClientsService
    {
        private readonly AppDb dbContext;
        private readonly IMapper mapper;
        private ILogger logger;

        public ClientsService(AppDb dbContext, IMapper mapper, ILogger<ClientsService> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
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
            logger.Log(LogLevel.Information, "Clients Requested");
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
