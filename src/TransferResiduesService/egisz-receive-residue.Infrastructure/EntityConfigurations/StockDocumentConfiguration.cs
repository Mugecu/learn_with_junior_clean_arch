using egisz_receive_residue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace egisz_receive_residue.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Конфигуратор набора данных для <see cref="StockDocument"/>.
    /// </summary>
    public class StockDocumentConfiguration : IEntityTypeConfiguration<StockDocument>
    {
        public void Configure(EntityTypeBuilder<StockDocument> builder)
        {
            builder.ToTable("stock_document");

            builder.HasKey(m => m.Id);
        }
    }
}