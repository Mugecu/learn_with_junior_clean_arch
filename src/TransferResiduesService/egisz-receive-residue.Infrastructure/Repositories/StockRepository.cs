using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace egisz_receive_residue.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для данных типа <see cref="Stock"/> из БД рег. ФРЛЛО.
    /// </summary>
    /// <remarks>
    /// Конструктор репозитория.
    /// </remarks>
    /// <param name="pharmEgiszContext">Контекст данных БД рег. ФРЛЛО.</param>
    internal class StockRepository(PharmEgiszContext pharmEgiszContext) : IStockRepository
    {
        private readonly PharmEgiszContext _pharmEgiszContext = pharmEgiszContext;

        /// <summary>
        /// Получить код ОКАТО из конфигурации.
        /// </summary>
        /// <returns>Код ОКАТО из конфигурации.</returns>
        public string GetRegion()
        {
            return Bootstrap.Configuration.GetSection("CodOkato")?.Value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Добавить новые объекты <see cref="Stock"/> в контекст.
        /// </summary>
        /// <param name="stockEntities">Список объектов для вставки в stock в БД рег. ФРЛЛО.</param>
        /// <returns>Задача добавления объектов.</returns>
        public async Task AddStocksAsync(IEnumerable<Stock> stockEntities)
        {
            foreach (var stockEntity in stockEntities)
            {
                await _pharmEgiszContext.AddAsync(stockEntity);
            }
        }

        /// <summary>
        /// Получить данные из БД по фильтру guids.
        /// </summary>
        /// <param name="residuesGuids">Список guids из БД сервиса остатков для фильтрации.</param>
        /// <returns>Сгруппированные данные, где:
        /// - Первая группа (true) содержит записи, присутствующие и в БД рег. ФРЛЛО, и в БД сервиса остатков.
        /// - Вторая группа (false) содержит записи, присутствующие только в БД рег. ФРЛЛО.</returns>
        public async Task<List<IGrouping<bool, Stock>>> GetByFilterAsync(IEnumerable<Guid>? residuesGuids)
        {
            var condition = residuesGuids?.Any() ?? false;
            return await _pharmEgiszContext.Set<Stock>()
                .Include(m => m.StockDocument)
                .GroupBy(m => condition ? residuesGuids!.Contains(m.Guid) : false)
                .ToListAsync();
        }

        /// <summary>
        /// Сохранить изменения в БД.
        /// </summary>
        /// <returns>Задача сохранения объектов.</returns>
        public async Task SaveAsync()
        {
            await _pharmEgiszContext.SaveChangesAsync();
        }
    }
}