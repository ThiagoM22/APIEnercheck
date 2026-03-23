namespace APIEnercheck.Models
{
    public class AssinaturaPlano : ModelBase
    {
        public int PlanoId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataVencimento { get; set; }   

        public Plano Plano { get; set; }
        public Usuario Usuario { get; set; }
    }
}
