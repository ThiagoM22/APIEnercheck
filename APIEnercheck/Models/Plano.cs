using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIEnercheck.Models
{
    public class Plano : ModelBase
    {
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? QtdRequisicao { get; set; }
        public int? QtdUsers { get; set; }
    }
}
