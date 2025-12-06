namespace APIEnercheck.Models
{
    public class PagamentoPlano
    {
        public Guid PagamentoPlanoId { get; set; }
        public int PlanoId { get; set; }
        public string UsuarioId { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }

        public virtual Plano Plano { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
