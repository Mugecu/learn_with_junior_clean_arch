using egisz_receive_residue.Application;
using egisz_receive_residue.Domain.Interfaces;
using egisz_receive_residue.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace egisz_receive_residue.Infrastructure
{
    /// <summary>
    /// Статический класс для настройки и инициализации основных компонентов приложения.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Глобальная конфигурация приложения.
        /// </summary>
        public static IConfiguration Configuration => _configuration;
        /// <summary>
        /// Внутреннее поле для хранения глобальной конфигурации.
        /// </summary>
        private static IConfiguration _configuration { get; set; }
        /// <summary>
        /// Установить глобальную конфигурацию приложения.
        /// </summary>
        /// <param name="configuration">Конфигурация приложения.</param>
        public static void SetConfiguration(IConfiguration configuration) => _configuration = configuration;

        /// <summary>
        /// Зарегистрировать варианты использования (use cases) в приложении.
        /// </summary>
        /// <returns>Массив сборок, содержащих варианты использования.</returns>
        public static IEnumerable<Assembly> AddAssembliesWithResiduesUseCases()
            => new List<Assembly>{ Assembly.GetAssembly(typeof(ApplicationReference)) };

        /// <summary>
        /// Добавить репозитории в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов из конфигурации.</param>
        /// <returns>Обновленная коллекция сервисов.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IResidueRepository, ResidueRepository>();
            services.AddTransient<IStockRepository, StockRepository>();
            services.AddTransient<IStockDocumentRepository, StockDocumentRepository>();
            return services;
        }
    }
}