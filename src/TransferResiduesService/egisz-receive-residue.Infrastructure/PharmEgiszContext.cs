using egisz_receive_residue.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace egisz_receive_residue.Infrastructure
{
    /// <summary>
    /// Контекст данных БД рег. ФРЛЛО.
    /// </summary>
    public class PharmEgiszContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста данных.
        /// </summary>
        /// <param name="options">Опции подключения к базе данных.</param>
        public PharmEgiszContext(DbContextOptions<PharmEgiszContext> options) : base(options) { }

        /// <summary>
        /// Конфигурирование модели данных контекста.
        /// </summary>
        /// <param name="modelBuilder">Конструктор модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new StockDocumentConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Конфигурирование опций подключения к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Конструктор опций.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}