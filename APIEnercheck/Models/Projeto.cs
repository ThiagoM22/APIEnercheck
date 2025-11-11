namespace APIEnercheck.Models
{
    public class Projeto
    {
        public Guid ProjetoId { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime dataInicio { get; set; }
        public int? Progresso { get; set; }
        public string? Analise { get; set; }

    }
}
