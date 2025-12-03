using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.DTOs.Planos
{
    public class PlanoResponseDto
    {
        public int PlanoId { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? QuantidadeReq { get; set; }
        public bool? Ativo { get; set; }
        public int? QuantidadeUsers { get; set; }
    }
}
