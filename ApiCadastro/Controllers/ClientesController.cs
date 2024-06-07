using ApiCadastro.Models;
using ApiCadastro.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] ApiContext context)
        {
            return Ok(await context.Clientes.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromServices] ApiContext context, [FromRoute] int id)
        {
            var cliente = await context.Clientes.AsNoTracking().Where(x => x.Id == id).Include(x => x.Logradouros).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] ApiContext context, Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Requisição inválida");

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromServices] ApiContext context, [FromRoute] int id, Cliente cliente)
        {
            var clienteLocalizado = await context.Clientes.FindAsync(id);
            if (clienteLocalizado != null)
            {
                clienteLocalizado.Nome = cliente.Nome;
                
                await context.SaveChangesAsync();
                return Ok(clienteLocalizado);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCliente([FromServices] ApiContext context, int id)
        {
            var cliente = await context.Clientes.Where(x=>x.Id == id).Include(x=>x.Logradouros).FirstOrDefaultAsync();
            if (cliente != null)
            {
                context.Logradouros.RemoveRange(cliente.Logradouros);
                context.Clientes.Remove(cliente);
                context.SaveChanges();
                return Ok(cliente);
            }
            return NotFound();
        }
    }
}
