using APIEnercheck.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIEnercheck.Data
{
    public class ApiDbContext : IdentityDbContext<Usuario>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) :
            base(options)
        { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Projeto> Projeto { get; set; } = default!;
        public DbSet<PagamentoPlano> PagamentoPlanos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define explicitamente o tipo de coluna para evitar truncamento de decimais
            modelBuilder.Entity<PagamentoPlano>()
                .Property(p => p.ValorPago)
                .HasColumnType("decimal(10,2)");
        }
    }
}
