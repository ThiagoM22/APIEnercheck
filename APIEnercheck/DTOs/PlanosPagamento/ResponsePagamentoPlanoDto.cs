namespace APIEnercheck.DTOs.PlanosPagamento
{
    public class ResponsePagamentoPlanoDto
    {
        public Guid PagamentoPlanoId { get; set; }
        public int PlanoId { get; set; }
        public string UsuarioId { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal? ValorPago { get; set; }
        public string? PlanoNome { get; set; }
        public string? UsuarioNome { get; set; }

    }
}
