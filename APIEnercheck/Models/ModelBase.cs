namespace APIEnercheck.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }
        public bool Ativo { get; set; } 
        public DateTime DataCriacao { get; set; }
    }
}
