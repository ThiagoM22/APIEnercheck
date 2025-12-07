using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIEnercheck.Models
{
    public class Plano
    {
        public int PlanoId { get; set; }
        [Required(ErrorMessage = "O nome do plano é obrigatório")]
        [Display(Name = "Nome do Plano")]
        [StringLength(100, ErrorMessage = "Você ultrapassou a quantidade mínima de caracteres")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O valor do plano é obrigatório")]
        [Display(Name = "Valor do Plano")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Preco { get; set; }
        [Required(ErrorMessage = "A quantidade de requisições do plano é obrigatório")]
        [Display(Name = "Total de Requisições")]
        [Range(0, 200)]
        public int? QuantidadeReq { get; set; }
        public int? QuantidadeUsers { get; set; }

    }
}
