using Microsoft.EntityFrameworkCore;

namespace egisz_receive_residue.Infrastructure
{
    /// <summary>
    /// Контекст данных БД сервиса остатков.
    /// </summary>
    public class PharmacyResidueContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста данных.
        /// </summary>
        /// <param name="options">Опции подключения к базе данных.</param>
        public PharmacyResidueContext(DbContextOptions<PharmacyResidueContext> options) : base(options) { }

        /// <summary>
        /// Конфигурирование модели данных контекста.
        /// </summary>
        /// <param name="modelBuilder">Конструктор модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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