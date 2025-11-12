using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEnercheck.Data;
using APIEnercheck.Models;
using System.ComponentModel.DataAnnotations;

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

        //DTOs para criação e resposta

        //Define os dados necessários para criar um projeto.
        public class ProjetoCreateDto
        {
            [Required]
            public string UsuarioId { get; set; }
            [Required]
            public string Nome { get; set; }
            [Required]
            public string Descricao { get; set; }
            public DateTime dataInicio { get; set; }
            public int? Progresso { get; set; }
            public string? Status { get; set; }
            public string? Analise { get; set; }
        }


        //Define os dados que serão retornados ao cliente, evitando ciclos de referencia.
        public class ProjetoResponseDto
        {
            public Guid ProjetoId { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public DateTime dataInicio { get; set; }
            public int? Progresso { get; set; }
            public string? Status { get; set; }
            public string? Analise { get; set; }
            public string UsuarioId { get; set; }
            public string? UsuarioNome { get; set; }
        }
        // POST: api/Projetos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projeto>> PostProjeto([FromBody] ProjetoCreateDto dto)
        {
            //Busca o usuario
            var usuario = await _context.Usuarios
                .Include(u => u.Plano)
                .FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);
            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            //Verifica se o usuario possui um plano
            if (usuario.Plano == null)
                return BadRequest("Usuário não possui um plano");

            usuario.Plano.QuantidadeReq--;

            //Cria o projeto
            var projeto = new Projeto
            {
                UsuarioId = dto.UsuarioId,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                dataInicio = dto.dataInicio,
                Progresso = dto.Progresso,
                Status = dto.Status,
                Analise = dto.Analise,
            };
            //Salva so projeto no banco
            _context.Projeto.Add(projeto);
            await _context.SaveChangesAsync();

            //Retorna um DTO com os dados do projeto e o nome do usuário, evitando ciclos de referência
            var response = new ProjetoResponseDto
            {
                ProjetoId = projeto.ProjetoId,
                Nome = projeto.Nome,
                Descricao = projeto.Descricao,
                dataInicio = projeto.dataInicio,
                Progresso = projeto.Progresso,
                Status = projeto.Status,
                Analise = projeto.Analise,
                UsuarioId = projeto.UsuarioId,
                UsuarioNome = usuario.NomeCompleto
            };

            return CreatedAtAction("GetProjeto", new { id = projeto.ProjetoId }, response);
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
