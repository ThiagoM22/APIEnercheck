namespace APIEnercheck.Models
{
    public class PlanoPago : ModelBase
    {
        public string? UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime DataPagamento { get; set; }

        public Usuario? Usuario { get; set; }
        public Plano? Plano { get; set; }
    }
}
