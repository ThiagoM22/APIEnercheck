using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.DTOs.Planos
{
    public class PutPlanosDto
    {
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? QuantidadeReq { get; set; }
    }
}
