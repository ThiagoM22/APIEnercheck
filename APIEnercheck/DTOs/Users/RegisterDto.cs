using System.ComponentModel.DataAnnotations;

namespace APIEnercheck.DTOs.Users
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "O email pe obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome de exibição deve ter no máximo 100 caracteres")]
        public string NomeCompleto { get; set; }
        public string NumeroCrea { get; set; }
        public string? Empresa { get; set; }
    }
}
