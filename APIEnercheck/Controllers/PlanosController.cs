using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEnercheck.Data;
using APIEnercheck.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PlanosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Planos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlanos()
        {
            return await _context.Planos.ToListAsync();
        }

        // GET: api/Planos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> GetPlano(int id)
        {
            var plano = await _context.Planos.FindAsync(id);

            if (plano == null)
            {
                return NotFound();
            }

            return plano;
        }

        // PUT: api/Planos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlano(int id, Plano plano)
        {
            if (id != plano.PlanoId)
            {
                return BadRequest();
            }

            _context.Entry(plano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoExists(id))
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

        // POST: api/Planos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Plano>> PostPlano(Plano plano)
        {
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlano", new { id = plano.PlanoId }, plano);
        }

        // DELETE: api/Planos/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlano(int id)
        {
            var plano = await _context.Planos.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }

            _context.Planos.Remove(plano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanoExists(int id)
        {
            return _context.Planos.Any(e => e.PlanoId == id);
        }
    }
}
