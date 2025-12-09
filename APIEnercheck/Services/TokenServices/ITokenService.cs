namespace APIEnercheck.Services.TokenServices
{
    public interface  ITokenService
    {
        string GenerateJwtToken(Models.Usuario user);
    }
}
