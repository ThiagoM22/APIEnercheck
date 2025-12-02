namespace APIEnercheck.DTOs.Users
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroCrea { get; set; }
        public string Empresa { get; set; }
        public List<string> Roles { get; set; }
    }
}
