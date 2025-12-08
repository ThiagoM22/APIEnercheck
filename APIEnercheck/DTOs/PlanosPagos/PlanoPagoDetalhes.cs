namespace APIEnercheck.DTOs.PlanosPagos
{
    public class PlanoPagoDetalhes
    {
        public Guid PlanoPagoId { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? UsuarioId { get; set; }
        public int PlanoId { get; set; }
        public string? PlanoNome { get; set; }
        public string? NomeCompleto { get; set; }


    }
}
