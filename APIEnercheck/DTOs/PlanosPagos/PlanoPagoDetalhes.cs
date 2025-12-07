namespace APIEnercheck.DTOs.PlanosPagos
{
    public class PlanoPagoDetalhes
    {
        public Guid PlanoPagoId { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal? ValorTotal { get; set; }
        public Usuario? Usuario { get; set; }
        public Plano? Plano { get; set; }
    }
}
