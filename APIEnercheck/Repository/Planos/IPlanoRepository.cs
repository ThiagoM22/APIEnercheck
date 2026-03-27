using APIEnercheck.Models;

namespace APIEnercheck.Repository.Planos
{
    public interface IPlanoRepository
    {
        Task<IEnumerable<Plano>> ListarPlanos();
        Task<Plano> ObterPlanoPorId(Guid Id);
        Task<Plano> CriarNovoPlano(Plano plano);
        Task<Plano> AtualizarPlano(Plano plano);
        Task<bool> ExcluirPlano(Plano plano);
    }
}
