namespace APIEnercheck.DTOs.PlanosPagos
{
    public class PlanoPagoResponseDto
    {      
        public Guid PlanoPagoId { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
