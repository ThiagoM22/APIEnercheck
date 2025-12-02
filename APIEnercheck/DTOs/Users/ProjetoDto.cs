namespace APIEnercheck.DTOs.Users
{
    public class ProjetoDto
    {
        public Guid ProjetoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public string? Status { get; set; }
    }
}
