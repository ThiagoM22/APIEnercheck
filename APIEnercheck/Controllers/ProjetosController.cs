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
using Microsoft.AspNetCore.Authorization;
using APIEnercheck.Services;
using APIEnercheck.DTOs.Projetos;

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly GeminiService _geminiService;

        public ProjetosController(ApiDbContext context, GeminiService geminiService)
        {
            _context = context;
            _geminiService = geminiService;
        }

        // GET: api/Projetos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjeto()
        {
            return await _context.Projeto.ToListAsync();
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMeusProjetos()
        {
            var logadinho = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(logadinho))
                return Unauthorized("Usuario não autenticado");

            var meusProjetos = await _context.Projeto.Where(p => p.UsuarioId == logadinho).ToListAsync();

            var ProjetosDetalhe = meusProjetos.Select(m => new ProjetosDetalhe
            {
                ProjetoId = m.ProjetoId,
                Nome = m.Nome,
                Descricao = m.Descricao,
                dataInicio = m.dataInicio,
                Progresso = m.Progresso,
                Status = m.Status,
                Analise = m.Analise
            }).ToList();

            return Ok(ProjetosDetalhe);

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
        public async Task<IActionResult> PutProjeto(Guid id, PutProjetosDto dto)
        {
            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto == null)
            {
                return NotFound("Projeto não existe");
            }

            projeto.Nome = dto.Nome ?? projeto.Nome;
            projeto.Descricao = dto.Descricao ?? projeto.Descricao;

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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProjetoResponseDto>> PostProjeto([FromBody] ProjetoCreateDto dto)
        {
            var logadinho = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(logadinho))
                return Unauthorized("Usuario não autenticado");

            var nowUtc = DateTime.UtcNow;

            //Busca o usuario
            var usuario = await _context.Usuarios
                .Include(u => u.Plano)
                .FirstOrDefaultAsync(u => u.Id == logadinho);
            if (usuario == null)
                return BadRequest("Usuário não encontrado");

            //Verifica se o usuario possui um plano
            if (usuario.Plano == null)
                return BadRequest("Usuário não possui um plano");

            if(usuario.DataVencimentoPlano <= nowUtc)
            {
                return BadRequest("Seu plano está vencido");
            }

            if (usuario.UserReq == 0)
            {
                return BadRequest("Você não tem requisições sufucientes");
            }

            usuario.UserReq--;

            //Cria o projeto
            var projeto = new Projeto
            {
                UsuarioId = logadinho,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                dataInicio = DateTime.Now,
                Progresso = 0,
                Status = "Pendente",
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


        [HttpPost("projeto/{id}/analisar")]
        public async Task<IActionResult> AnalisarProjeto(Guid id, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {

            }

            var projeto = await _context.Projeto.FindAsync(id);

            if (projeto == null)
            {

            }

            // Lembrar de a requisição ser reduzida na análise depois, pra testar.
            // usuario.userReq --;

            try
            {
                using var memoryStream = new MemoryStream();
                await arquivo.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();


                string analiseJson = await _geminiService.AnalisarImagemAsync(
                    imageBytes,
                    arquivo.ContentType,
                    projeto.Descricao
                    );

                projeto.Analise = analiseJson;
                projeto.Status = "Analisado";

                _context.Entry(projeto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(projeto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao analisar o projeto: " + ex.Message);
            }


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
