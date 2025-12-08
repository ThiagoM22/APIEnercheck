using APIEnercheck.Data;
using APIEnercheck.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using APIEnercheck.DTOs.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthorizationService _authorizationService;
        // Colocar DbContext
        public UsuariosController(ApiDbContext context, UserManager<Usuario> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            this._authorizationService = authorizationService;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            //Carrega o plano e os projetos vinculados ao usuario, evitando multiplas consultas ao banco e garante que os dados estejam disponiveis para o mapeamento.
            var usuarios = await _context.Usuarios
    .Include(u => u.Plano)
    .Include(u => u.Projetos)
    .ToListAsync();


            //Mapeia o DTO, criando para cada usuario um objeto UsuarioDetalhesDTO, preenchendo os dados básicos do usuario, e caso ele tenha um plano e umprojeto, cria um DTO deles
            var result = new List<UsuarioDetalhesDto>();

            foreach (var u in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(u);

                result.Add(new UsuarioDetalhesDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    NomeCompleto = u.NomeCompleto,
                    NumeroCrea = u.NumeroCrea,
                    Empresa = u.Empresa,
                    UseReq = u.UserReq,
                    Plano = u.Plano == null ? null : new PlanoDto
                    {
                        PlanoId = u.Plano.PlanoId,
                        Nome = u.Plano.Nome,
                        Preco = u.Plano.Preco
                    },
                    Projetos = u.Projetos?.Select(p => new ProjetoDto
                    {
                        ProjetoId = p.ProjetoId,
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        dataInicio = p.dataInicio,
                        Status = p.Status
                    }).ToList() ?? new List<ProjetoDto>(),
                    Roles = roles.ToList()
                });
            }


            return Ok(result);
        }


        [HttpPost("roles")]
        public async Task<IActionResult> CriarRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("O nome da role é obrogatório");

            var rolesExiste = await _context.Roles.AnyAsync(r => r.Name == roleName);
            if (rolesExiste)
            {
                return BadRequest("Essa role já existe");
            }
            var roleManager = HttpContext.RequestServices.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null)
                return StatusCode(500, "RolaMagager não dispoivel");
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{roleName}' criada com sucesso.");
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUsuarioLogado()
        {
            // Tenta obter o ID do usuário logado dos claims
            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuário não autenticado.");

            // Busca o usuário no banco, incluindo plano e projetos
            var usuario = await _context.Usuarios
                .Include(u => u.Plano)
                .Include(u => u.Projetos)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            //Armazenando a role do usuario logado
            var roles = await _userManager.GetRolesAsync(usuario);

            //Verificando se a role Existe
            if (usuario == null)
                return NotFound("Usuário não encontrado.");

            // Monta o DTO de resposta
            var usuarioDto = new UsuarioDetalhesDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                NomeCompleto = usuario.NomeCompleto,
                NumeroCrea = usuario.NumeroCrea,
                Empresa = usuario.Empresa,
                UseReq = usuario.UserReq,
                Plano = usuario.Plano == null ? null : new PlanoDto
                {
                    PlanoId = usuario.Plano.PlanoId,
                    Nome = usuario.Plano.Nome,
                    Preco = usuario.Plano.Preco
                },
                Projetos = usuario.Projetos?.Select(p => new ProjetoDto
                {
                    ProjetoId = p.ProjetoId,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    dataInicio = p.dataInicio,
                    Status = p.Status
                }).ToList() ?? new List<ProjetoDto>(),
                Roles = roles.ToList()

            };

            return Ok(usuarioDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdUsers(string id)
        {
            var user = await _context.Usuarios.FindAsync(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            return Ok(user);
        }


        [HttpPost("Cliente")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Crie uma nova instância de usuarios
            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                NumeroCrea = model.NumeroCrea,
                Empresa = model.Empresa,
                EmailConfirmed = true
            };

            // Crie o usuário com a senha usando o UserManager
            var result = await _userManager.CreateAsync(user, model.Senha);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Cliente"); // ou "Admin", conforme sua lógica
                var rolas = await _userManager.GetRolesAsync(user);

                return CreatedAtAction(nameof(GetUsuarios), new { id = user.Id }, new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    NomeCompleto = user.NomeCompleto,
                    NumeroCrea = user.NumeroCrea,
                    Empresa = user.Empresa,
                    Roles = rolas.ToList()
                });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> RegisterAmin([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Crie uma nova instância de usuarios
            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                NumeroCrea = model.NumeroCrea,
                Empresa = model.Empresa,
                EmailConfirmed = true
            };

            // Crie o usuário com a senha usando o UserManager
            var result = await _userManager.CreateAsync(user, model.Senha);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin"); // ou "Admin", conforme sua lógica
                var rolas = await _userManager.GetRolesAsync(user);

                return CreatedAtAction(nameof(GetUsuarios), new { id = user.Id }, new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    NomeCompleto = user.NomeCompleto,
                    NumeroCrea = user.NumeroCrea,
                    Empresa = user.Empresa,
                    Roles = rolas.ToList()
                });
            }
            return BadRequest(result.Errors);
        }


        // PUT api/<UsuariosController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(string id, [FromBody] PutUsuariosDto dto, [FromServices] IAuthorizationService authorizationService)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, id, "AdminOrOwner");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var usuarioesxite = await _context.Usuarios.FindAsync(id);
            if (usuarioesxite == null)
            {
                return NotFound();
            }

            usuarioesxite.NomeCompleto = dto.NomeCompleto ?? usuarioesxite.NomeCompleto;
            usuarioesxite.Email = dto.Email ?? usuarioesxite.Email;

            _context.Entry(usuarioesxite).State = EntityState.Modified;


            var userExistente = await _userManager.FindByIdAsync(id);

            if (userExistente == null)
            {

                return NotFound("Usuario não encontrado");
            }

            userExistente.NomeCompleto = dto.NomeCompleto ?? userExistente.NomeCompleto;
            userExistente.Email = dto.Email ?? userExistente.Email;

            var result = await _userManager.UpdateAsync(userExistente);

            if (!result.Succeeded)
            {
                return BadRequest("Erro ao atualizar o usuário. " + result.Errors);
            }

            return NoContent();
        }

        [HttpPut("me")]
        public async Task<IActionResult> PutUsuarioLogado([FromBody] PutUsuariosDto dto)
        {
            var userLogadoId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userLogadoId == null)
            {
                NotFound("Usuario não logado");
            }
            var usuarioesxite = await _context.Usuarios.FindAsync(userLogadoId);
            if (usuarioesxite == null)
            {
                return NotFound();
            }
            usuarioesxite.NomeCompleto = dto.NomeCompleto ?? usuarioesxite.NomeCompleto;
            usuarioesxite.Email = dto.Email ?? usuarioesxite.Email;
            _context.Entry(usuarioesxite).State = EntityState.Modified;

            var userExistente = await _userManager.FindByIdAsync(userLogadoId);
            if (userExistente == null)
            {
                return NotFound("Usuario não encontrado");
            }
            userExistente.NomeCompleto = dto.NomeCompleto ?? userExistente.NomeCompleto;
            userExistente.Email = dto.Email ?? userExistente.Email;
            var result = await _userManager.UpdateAsync(userExistente);
            if (!result.Succeeded)
            {
                return BadRequest("Erro ao atualizar o usuário. " + result.Errors);
            }
            return NoContent();
        }

        [HttpPut("add/plano")]
        public async Task<IActionResult> VincularPlanoAoUsuario([FromBody] int planoId)
        {

            // Pega o ID do usuário atual
            var userLogadoId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userLogadoId == null)
            {
                NotFound("Usuario não logado");
            }


            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == userLogadoId);
            if (usuario == null)
            {
                return NotFound("Usuário não logado ou não encontrado.");
            }
            //Analisa pelo id do plano se ele existe ou não
            var plano = await _context.Planos.FindAsync(planoId);
            if (plano == null)
            {
                return NotFound("Plano não encontrado");
            }

            if (usuario.Plano == null)
            { 
            plano.QuantidadeUsers++;
            _context.Entry(plano).State = EntityState.Modified;
                }

            //Atualiza o ususario com o novo plano
            usuario.PlanoId = planoId;
            usuario.Plano = plano;
            usuario.UserReq = plano.QuantidadeReq ?? 0;
            usuario.PlanoAtivo = true;
            usuario.DataInicioPlano = DateTime.UtcNow;
            usuario.DataVencimentoPlano = DateTime.UtcNow.AddMonths(1);
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            //Retorna os novos dados do usuario
            return Ok(new
            {
                usuario.Id,
                usuario.Email,
                usuario.NomeCompleto,
                usuario.PlanoId,
                usuario.UserReq,
                usuario.DataInicioPlano,
                usuario.DataVencimentoPlano,
                Plano = new { plano.PlanoId, plano.Nome, plano.Preco, plano.QuantidadeUsers }
            });

        }

        // DELETE api/<UsuariosController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();

        }
    }
}
