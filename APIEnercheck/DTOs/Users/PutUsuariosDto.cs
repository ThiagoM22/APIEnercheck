using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.DTOs.Users
{
    public class PutUsuariosDto
    {
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroCrea { get; set; }
        public string? Empresa { get; set; }
    }
}
