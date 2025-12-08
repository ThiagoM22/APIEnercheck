using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEnercheck.Data;
using APIEnercheck.Models;
using APIEnercheck.DTOs.PlanosPagos;

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoPagosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PlanoPagosController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PlanoPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoPago>>> GetPlanosPagos()
        {
            return await _context.PlanosPagos.ToListAsync();
;       }

        [HttpGet("me")]
        public async Task<IActionResult> GetMeusPlanoPago()
        {
            var logadinho = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(logadinho))
            {
                return Unauthorized("Usuario não logado");
            }

            var planoPago = await _context.PlanosPagos
                .Where(p => p.UsuarioId == logadinho)
                .ToListAsync();

            var planoPagoDetalhes = planoPago.Select(p => new PlanoPagoDetalhes
            {
                PlanoPagoId = p.PlanoPagoId,
                DataPagamento = p.DataPagamento,
                ValorTotal = p.ValorTotal,
                UsuarioId = p.UsuarioId,
                PlanoId = p.PlanoId,
                PlanoNome = p.Plano != null ? p.Plano.Nome : null,
                NomeCompleto = p.Usuario != null ? p.Usuario.NomeCompleto : null
            }).ToList();

            return Ok(planoPagoDetalhes);
        }

        // GET: api/PlanoPagos/5
        [HttpGet("{id}", Name = "GetPlanoPagoById")]
        public async Task<ActionResult<PlanoPago>> GetPlanoPago(Guid id)
        {
            var planoPago = await _context.PlanosPagos.FindAsync(id);

            if (planoPago == null)
            {
                return NotFound();
            }

            return planoPago;
        }

        // POST: api/PlanoPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlanoPago>> PostPlanoPago(PlanoPagoCreateDto dto)
        {
            var logadinho = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

         if (string.IsNullOrEmpty(logadinho))
            {
                return Unauthorized("Usuario não logado");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == logadinho);
            if (usuario == null)
            {
                return Unauthorized("Usuario não encontrado");
            }
            var plano = await _context.Planos.FindAsync(dto.PlanoId);

            if (plano == null)
            {
                return BadRequest("Plano não encontrado");
            }

            var planoPago = new PlanoPago
            { 
                UsuarioId = logadinho,
                PlanoId = dto.PlanoId,
                ValorTotal = plano.Preco,
                DataPagamento = DateTime.UtcNow
            };
            _context.PlanosPagos.Add(planoPago);
            await _context.SaveChangesAsync();

            var response = new PlanoPagoResponseDto
            {
                PlanoPagoId = planoPago.PlanoPagoId,
                ValorTotal = planoPago.ValorTotal,
                DataPagamento = planoPago.DataPagamento,
            };

            // Retorno corrigido usando nameof para evitar erros de digitação
            return CreatedAtRoute("GetPlanoPagoById", new { id = planoPago.PlanoPagoId }, response);
        }

        // DELETE: api/PlanoPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanoPago(Guid id)
        {
            var planoPago = await _context.PlanosPagos.FindAsync(id);
            if (planoPago == null)
            {
                return NotFound();
            }

            _context.PlanosPagos.Remove(planoPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanoPagoExists(Guid id)
        {
            return _context.PlanosPagos.Any(e => e.PlanoPagoId == id);
        }
    }
}
