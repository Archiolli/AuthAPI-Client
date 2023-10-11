using AppClients.Data;
using AppClients.Models;
using AppClients.Services;
using AppClients.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppClients.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost("/cliente")]
        [Authorize(Policy = "Colaborador")]
        public async Task<IActionResult> PostAddClientAsync(
            [FromBody] Client model,
            [FromServices] AppDbContext context
            )
        {            
            var client = new Client
            {
                IdColaborador = model.IdColaborador,
                Name = model.Name,
                Obs = model.Obs
            };

            Console.WriteLine(client);
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();

            return Created($"/{client.Id}", client);
        }

        [HttpGet("/clientes/{id:int}")]
        [Authorize(Policy = "Colaborador")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var clients = await context.Clients
                .Where(t => t.IdColaborador == id)
                .ToListAsync();

            if (clients is null) return NotFound();

            return Ok(clients);
        }
    }
}

