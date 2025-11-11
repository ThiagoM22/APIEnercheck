using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEnercheck.Data;
using APIEnercheck.Models;

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ProjetosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Projetos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjeto()
        {
            return await _context.Projeto.ToListAsync();
        }

        // GET: api/Projetos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projeto>> GetProjeto(Guid id)
        {
            var projeto = await _context.Projeto.FindAsync(id);

            if (projeto == null)
            {
                return NotFound();
            }

            return projeto;
        }

        // PUT: api/Projetos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjeto(Guid id, Projeto projeto)
        {
            if (id != projeto.ProjetoId)
            {
                return BadRequest();
            }

            _context.Entry(projeto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projetos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projeto>> PostProjeto(Projeto projeto)
        {
            _context.Projeto.Add(projeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjeto", new { id = projeto.ProjetoId }, projeto);
        }

        // DELETE: api/Projetos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto(Guid id)
        {
            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }

            _context.Projeto.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjetoExists(Guid id)
        {
            return _context.Projeto.Any(e => e.ProjetoId == id);
        }
    }
}
