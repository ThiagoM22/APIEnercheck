using Microsoft.AspNetCore.Identity;

namespace APIEnercheck.Models
{
    public class Usuario : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public string? NumeroCrea { get; set; }
        public string? Empresa { get; set; }  
        
        public Guid? AssinaturaPlanoId { get; set; }
        public AssinaturaPlano? AssinaturaPlano { get; set; }

        public int PlanoId { get; set; }
        public Plano? Plano { get; set; }

        public List<PlanoPago>? Pagamentos { get; set; }
        public ICollection<Projeto>? Projetos { get; set; }
    }
}
