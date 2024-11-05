using Microsoft.EntityFrameworkCore;

namespace WebProcuderes.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Produto> Produtos { get; set; }
        public object Produto { get; internal set; }
    }
}
