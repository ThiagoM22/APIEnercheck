using Microsoft.AspNetCore.Identity;

namespace APIEnercheck.Models
{
    public class Usuario : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public ICollection<Projeto> Projetos { get; set; }
        public string? NumeroCrea { get; set; }
        public int? PlanoId { get; set; }
        public Plano? Plano { get; set; }
        public int UserReq { get; set; }
        public string? Empresa { get; set; }
        public Usuario() : base() { }
    }
}
