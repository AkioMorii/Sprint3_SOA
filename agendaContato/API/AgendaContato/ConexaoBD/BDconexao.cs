using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatos.Conexao
{
    public class DataBaseConexao : DbContext
    {
        public DataBaseConexao(DbContextOptions<DataBaseConexao> options) : base(options) { }

        public DbSet<Contatos> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contatos>().ToTable("contatos");
        }
    }
}
