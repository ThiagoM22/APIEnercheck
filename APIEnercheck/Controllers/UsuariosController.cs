using APIEnercheck.Data;
using APIEnercheck.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            public string? Empresa { get; set; }
            public PlanoDto Plano { get; set; }
            public List<ProjetoDto> Projetos { get; set; }
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
            var result = usuarios.Select(u => new UsuarioDetalhesDto
            {
                Id = u.Id,
                Email = u.Email,
                NomeCompleto = u.NomeCompleto,
                NumeroCrea = u.NumeroCrea,
                Empresa = u.Empresa,
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
                }).ToList() ?? new List<ProjetoDto>()
            }).ToList();


            return Ok(result);
        }

        // GET api/<UsuariosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UsuariosController>
        [HttpPost]
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

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetUsuarios), new { id = user.Id }, new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                NomeCompleto = user.NomeCompleto,
                NumeroCrea = user.NumeroCrea,
                Empresa = user.Empresa,
            });
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

            public string NumeroCrea { get; set; }
            public string Empresa { get; set; }
        }

        public class UserResponseDto
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string NomeCompleto { get; set; }
            public string NumeroCrea { get; set; }
            public string Empresa { get; set; }
        }

        // PUT api/<UsuariosController>/5
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

        [HttpPut("{id}/plano")]
        public async Task<IActionResult> VincularPlanoAoUsuario(string id, [FromBody] int planoId)
        {
            //Analisa por meio do id do usuario se ele existe ou não
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
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
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            //Retorna os novos dados do usuario
            return Ok(new
            {
                usuario.Id,
                usuario.Email,
                usuario.NomeCompleto,
                usuario.PlanoId,
                Plano = new { plano.PlanoId, plano.Nome, plano.Preco }
            });

        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
