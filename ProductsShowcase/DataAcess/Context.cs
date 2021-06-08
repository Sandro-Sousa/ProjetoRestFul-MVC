using DataAcess.Mappings;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAcess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoMappings());
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
