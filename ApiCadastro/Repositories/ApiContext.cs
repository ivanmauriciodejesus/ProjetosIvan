using ApiCadastro.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastro.Repositories
{
    public class ApiContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApiContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Consultor> Consultores { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Logradouro> Logradouros { get; set; }
    }
}
