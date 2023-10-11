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
    public class ProdutoController : ControllerBase
    {
        [HttpPost("/login")]
        public async Task<IActionResult> PostAsync(
            [FromBody] User model,
            [FromServices] AppDbContext context
        )
        {
            var user = await context.Users
                .FirstOrDefaultAsync(t =>
                    t.UserName == model.UserName &&
                    t.Password == model.Password
                );

            if (user == null)
            {
                user = UserReposity.Get(model.UserName, model.Password);
                if (user == null)
                {
                    return NotFound(new { message = "Usuário ou senha inválidos" });
                }
            }

            var token = TokenService.GenerateToken(user);
            return Ok(new
            {
                user = user,
                token = token
            });
        }

        [HttpGet("/users")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context
            )
        {
            return Ok(await context.Users.ToListAsync());
        }

        [HttpGet("/autenticado")]
        [Authorize] // Para exigir autenticação
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            var mensagem = "Autenticado como " + User.Identity.Name;

            return Ok(mensagem);
        }

        [HttpPost("/colaborador")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> PostAddAsync(
            [FromBody] User model,
            [FromServices] AppDbContext context
            )
        {
            var user = new User
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = TypeUser.Colaborador
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Created($"/{user.Id}", user);
        }
    }
}