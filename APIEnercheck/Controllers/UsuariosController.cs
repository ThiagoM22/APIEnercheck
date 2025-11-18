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


        //Essas classes servem para estruturar os dados que serão enviados na resposta da API, evitando ciclos de referência e explondo apenas o necessário
        public class UsuarioDetalhesDto
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string NomeCompleto { get; set; }
            public string NumeroCrea { get; set; }
            public int? UseReq { get; set; }
            public string? Empresa { get; set; }
            public PlanoDto Plano { get; set; }
            public List<ProjetoDto> Projetos { get; set; }
            public List<String> Roles { get; set; }
        }

        public class PlanoDto
        {
            public int PlanoId { get; set; }
            public string Nome { get; set; }
            public decimal? Preco { get; set; }
        }

        public class ProjetoDto
        {
            public Guid ProjetoId { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public DateTime dataInicio { get; set; }
            public string? Status { get; set; }
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

        [Authorize(Roles = "Admin")]
        [HttpPost("roles")]
        public async Task<IActionResult> CriarRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("O nome da rola é obrogatório");

            var rolasExiste = await _context.Roles.AnyAsync(r => r.Name == roleName);
            if (rolasExiste)
            {
                return BadRequest("Essa rola já existe");
            }
            var rolasManager = HttpContext.RequestServices.GetService<RoleManager<IdentityRole>>();
            if (rolasManager == null)
                return StatusCode(500, "RolaMagager não dispoivel");
            var result = await rolasManager.CreateAsync(new IdentityRole(roleName));
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

        // GET api/<UsuariosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UsuariosController>
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


        public class RegisterDto
        {
            [Required(ErrorMessage = "O email pe obrigatório")]
            [EmailAddress(ErrorMessage = "Formato de email inválido")]
            public string Email { get; set; }

            [Required(ErrorMessage = "A senha é obrigatória")]
            [DataType(DataType.Password)]
            public string Senha { get; set; }

            [Required(ErrorMessage = "O nome de usuário é obrigatório")]
            [StringLength(100, ErrorMessage = "O nome de exibição deve ter no máximo 100 caracteres")]
            public string NomeCompleto { get; set; }
            public int UserReq { get; set; }

            public string NumeroCrea { get; set; }
            public string? Empresa { get; set; }
        }

        public class UserResponseDto
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string NomeCompleto { get; set; }
            public string NumeroCrea { get; set; }
            public string Empresa { get; set; }
            public List<string> Roles { get; set; }
        }

        // PUT api/<UsuariosController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(string id, Usuario usuarios, [FromServices] IAuthorizationService authorizationService)
        {
            if (id != usuarios.Id)
            {
                return BadRequest();
            }

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

            usuarioesxite.NomeCompleto = usuarios.NomeCompleto;
            usuarioesxite.Email = usuarios.Email;

            _context.Entry(usuarioesxite).State = EntityState.Modified;


            var userExistente = await _userManager.FindByIdAsync(id);

            if (userExistente == null)
            {

                return NotFound("Usuario não encontrado");
            }

            userExistente.NomeCompleto = usuarios.NomeCompleto;
            userExistente.Email = usuarios.Email;

            var result = await _userManager.UpdateAsync(userExistente);

            if (!result.Succeeded)
            {
                return BadRequest("Erro ao atualizar o usuário. " + result.Errors);
            }

            return NoContent();
        }

        [HttpPut("usuario/add/plano")]
        public async Task<IActionResult> VincularPlanoAoUsuario([FromBody] int planoId)
        {

            // Pega o ID do usuário atual
            var userLogadoId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userLogadoId == null)
            {
                NotFound("Não sobrou NADA pro betinha");
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

            //Atualiza o ususario com o novo plano
            usuario.PlanoId = planoId;
            usuario.Plano = plano;
            usuario.UserReq = plano.QuantidadeReq ?? 0;
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
