using egisz_receive_residue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace egisz_receive_residue.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="Stock"/>.
    /// </summary>
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("stock");

            builder.HasKey(m => m.Id);
        }
    }
}