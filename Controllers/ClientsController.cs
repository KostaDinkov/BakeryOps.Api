using AutoMapper;
using BakeryOps.API.Models.DTOs;
using BakeryOps.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BakeryOps.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(ICrudService<ClientDTO> clientsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await clientsService.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var client = await clientsService.GetById(id);
            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientDTO newClient)
        {
            var id = await clientsService.Create(newClient);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (await clientsService.Delete(id))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> EditClient(ClientDTO update)
        {
            var clientId = await clientsService.Update(update);
            return Ok(clientId);
        }
    }
}