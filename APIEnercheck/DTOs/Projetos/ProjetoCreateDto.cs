using System.ComponentModel.DataAnnotations;
namespace APIEnercheck.DTOs.Projetos
{
    public class ProjetoCreateDto
    {
        public string? UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public int? Progresso { get; set; }
        public string? Status { get; set; }
        public string? Analise { get; set; }
    }
}
