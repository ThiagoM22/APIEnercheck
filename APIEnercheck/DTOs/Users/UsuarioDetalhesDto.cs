using static APIEnercheck.Controllers.UsuariosController;

namespace APIEnercheck.DTOs.Users
{
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
}
