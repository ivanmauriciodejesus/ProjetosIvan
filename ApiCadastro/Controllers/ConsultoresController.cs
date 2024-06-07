using ApiCadastro.Models;
using ApiCadastro.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoresController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] ApiContext context)
        {
            return Ok(await context.Consultores.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromServices] ApiContext context, [FromRoute] int id)
        {
            var consultor = await context.Consultores.FindAsync(id);
            if (consultor == null)
            {
                return NotFound();
            }
            return Ok(consultor);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] ApiContext context, Consultor consultor)
        {
            if (consultor == null)
                return BadRequest("Requisição inválida");

            context.Consultores.Add(consultor);
            await context.SaveChangesAsync();
            return Ok(consultor);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromServices] ApiContext context, [FromRoute] int id, Consultor consultor)
        {
            var consultorLocalizado = await context.Consultores.FindAsync(id);
            if (consultorLocalizado != null)
            {
                consultorLocalizado.Nome = consultor.Nome;
                
                await context.SaveChangesAsync();
                return Ok(consultorLocalizado);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteContact([FromServices] ApiContext context, int id)
        {
            var consultor = await context.Consultores.FindAsync(id);
            if (consultor != null)
            {
                context.Consultores.Remove(consultor);
                context.SaveChanges();
                return Ok(consultor);
            }
            return NotFound();
        }
    }
}
