using APIEnercheck.Data;
using APIEnercheck.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEnercheck.Repository.Planos
{
    public class PlanoRepository
    {
        private readonly ApiDbContext _context;

        public PlanoRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plano>> ListarPlanos()
        {
            return await _context.Planos.AsNoTracking().ToListAsync(); ;
        }

        public async Task<Plano> ObterPlanoPorId(Guid Id)
        {
            return await _context.Planos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Plano> CriarNovoPlano(Plano plano)
        {
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();
            return plano;
        }

        public async Task<Plano> AtualizarPlano(Plano plano)
        {
            _context.Planos.Update(plano);
            await _context.SaveChangesAsync();
            return plano;
        }

        public async Task<bool> ExcluirPlano(Plano plano)
        {
            _context.Planos.Remove(plano);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
