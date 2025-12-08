namespace APIEnercheck.DTOs.Projetos
{
    public class ProjetosDetalhe
    {
        public Guid ProjetoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public int? Progresso { get; set; }
        public string? Status { get; set; }
        public string? Analise { get; set; }
        public string UsuarioId { get; set; }
        public string? UsuarioNome { get; set; }
    }
}
