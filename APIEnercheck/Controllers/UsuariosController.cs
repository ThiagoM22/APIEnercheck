using APIEnercheck.Data;
using APIEnercheck.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIEnercheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthorizationService authorizationService;
        private readonly ApiDbContext _context;

        public UsuariosController(UserManager<Usuario> userManager, IAuthorizationService authorizationService, ApiDbContext context)
        {
            _userManager = userManager;
            this.authorizationService = authorizationService;
            _context = context;
        }



        // GET: api/<UsuariosController>
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("usuario/info/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            //var identityUser = await _userManager.FindByIdAsync(usuario.Id.ToString());

            return (usuario);

        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                Empresa = model.Empresa,
                NumeroCrea = model.NumeroCrea,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(usuario);
        }

        // PUT api/<UsuariosController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
