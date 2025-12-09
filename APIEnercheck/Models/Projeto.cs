using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.Models
{
    public class Projeto
    {
        public Guid ProjetoId { get; set; }
        [Required(ErrorMessage = "O nome do projeto é obrigatório")]
        [Display(Name = "Nome do projeto")]
        [StringLength(100, ErrorMessage = "")]
        public required string Nome { get; set; }
        [Display(Name = "Descrição do projeto")]
        [DataType(DataType.MultilineText)]
        public required string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime dataInicio { get; set; }
        public int? Progresso { get; set; }
        [AllowedValues("Aprovado", "Pendente", "Analisado", ErrorMessage = " O projeto só possui esses dois status ")]
        public string? Status { get; set; }

        public string? Analise { get; set; }

    }
}
