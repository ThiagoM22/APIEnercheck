namespace APIEnercheck.DTOs.PlanosPagamento
{
    public class CreatePagamentoPlanoDto
    {
        public Guid PlanoId { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal ValorPago { get; set; }
    }
}
