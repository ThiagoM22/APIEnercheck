using System.ComponentModel.DataAnnotations;
namespace APIEnercheck.DTOs.Projetos
{
    public class ProjetoCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
