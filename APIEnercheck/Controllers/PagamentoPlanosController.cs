using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEnercheck.Data;
using APIEnercheck.Models;
using APIEnercheck.DTOs.PlanosPagamento;

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoPlanosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PagamentoPlanosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PagamentoPlanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoPlano>>> GetPagamentoPlanos()
        {
            return await _context.PagamentoPlanos.ToListAsync();
        }

        // GET: api/PagamentoPlanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoPlano>> GetPagamentoPlano(Guid id)
        {
            var pagamentoPlano = await _context.PagamentoPlanos.FindAsync(id);

            if (pagamentoPlano == null)
            {
                return NotFound();
            }

            return pagamentoPlano;
        }

        // PUT: api/PagamentoPlanos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamentoPlano(Guid id, PagamentoPlano pagamentoPlano)
        {
            if (id != pagamentoPlano.PagamentoPlanoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamentoPlano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoPlanoExists(id))
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

        // POST: api/PagamentoPlanos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PagamentoPlano>> PostPagamentoPlano([FromBody] CreatePagamentoPlanoDto dto)
        {
            var logadinho = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
        if (string.IsNullOrEmpty(logadinho))
            {
                return Unauthorized("Usuário não autenticado");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == logadinho);
            if (usuario == null)
            {
                return Unauthorized("Usuario não encontrado");
            }

            var plano = await _context.Planos.FindAsync(dto.PlanoId);

            if (plano == null)
            {
                return BadRequest("Plano inválido");
            }

            var pagamentoPlano = new PagamentoPlano
            {
                UsuarioId = logadinho,
                DataPagamento = DateTime.UtcNow, 
                PlanoId = dto.PlanoId,
                ValorPago = plano.Preco,
            };

            _context.PagamentoPlanos.Add(pagamentoPlano);
            await _context.SaveChangesAsync();

            var response = new ResponsePagamentoPlanoDto
            {
                PagamentoPlanoId = pagamentoPlano.PagamentoPlanoId,
                PlanoId = pagamentoPlano.PlanoId,
                UsuarioId = pagamentoPlano.UsuarioId,
                DataPagamento = pagamentoPlano.DataPagamento,
                ValorPago = pagamentoPlano.ValorPago
            };

            return CreatedAtAction("GetPagamentoPlano", new { id = pagamentoPlano.PagamentoPlanoId },response);
        }

        // DELETE: api/PagamentoPlanos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamentoPlano(Guid id)
        {
            var pagamentoPlano = await _context.PagamentoPlanos.FindAsync(id);
            if (pagamentoPlano == null)
            {
                return NotFound();
            }

            _context.PagamentoPlanos.Remove(pagamentoPlano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoPlanoExists(Guid id)
        {
            return _context.PagamentoPlanos.Any(e => e.PagamentoPlanoId == id);
        }
    }
}
