using APIEnercheck.Models;
using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.DTOs.Projetos
{
    public class PutProjetosDto
    {
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
    }
}
