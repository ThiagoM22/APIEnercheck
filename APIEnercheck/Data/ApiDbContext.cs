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
    }
}
