using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcess.Mappings
{
    public class ProdutoMappings : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey("CodigoId");
            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Estoque).HasColumnName("estoque").IsRequired();
            builder.Property(x => x.Preco).HasColumnName("preco").HasPrecision(15, 2).IsRequired();
        }
    }
}
