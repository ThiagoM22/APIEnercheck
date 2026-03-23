using APIEnercheck.Enums;

namespace APIEnercheck.Models
{
    public class Projeto : ModelBase
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public required string UsuarioId { get; set; }
        public string? Analise { get; set; }
        public int Progresso { get; set; }
        public StatusProjeto? Status { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
