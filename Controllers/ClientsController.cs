using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.API.Models.DTOs;
using Orders.API.Services;
using Orders.Data;
using Orders.Models;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService clientsService;
        private readonly IMapper mapper;
        public ClientsController(IClientsService clientsService, IMapper mapper)
        {
            this.clientsService = clientsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await clientsService.GetAllClients();
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await clientsService.GetClientById(id);
            if(client == null) return NotFound();

            return Ok(client);
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientDTO newClient)
        {

            var id = await clientsService.AddClient(newClient);
            return Ok(id);


        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deletedId = await clientsService.DeleteClient(id);
            return Ok(deletedId);
        }

        [HttpPut]
        public async Task<IActionResult> EditClient(int id, ClientDTO update)
        {
            var clientId = await clientsService.UpdateClient(id, update);
            return Ok(clientId);
        }
    }
}
