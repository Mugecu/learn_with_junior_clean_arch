using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для данных типа <see cref="Stock"/> из БД рег. ФРЛЛО.
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// Получить данные из БД по фильтру guids.
        /// </summary>
        /// <param name="residuesGuids">Список guids из БД сервиса остатков для фильтрации.</param>
        /// <returns>Сгруппированные данные, где:
        /// - Первая группа (true) содержит записи, присутствующие и в БД рег. ФРЛЛО, и в БД сервиса остатков.
        /// - Вторая группа (false) содержит записи, присутствующие только в БД рег. ФРЛЛО.</returns>
        Task<List<IGrouping<bool, Stock>>> GetByFilterAsync(IEnumerable<Guid>? residuesGuids);

        /// <summary>
        /// Сохранить изменения в БД.
        /// </summary>
        /// <returns>Задача сохранения объектов.</returns>
        public Task SaveAsync();

        /// <summary>
        /// Добавить новые объекты <see cref="Stock"/> в контекст.
        /// </summary>
        /// <param name="stockEntities">Список объектов для вставки в stock в БД рег. ФРЛЛО.</param>
        /// <returns>Задача добавления объектов.</returns>
        Task AddStocksAsync(IEnumerable<Stock> stockEntities);

        /// <summary>
        /// Получить код ОКАТО из конфигурации.
        /// </summary>
        /// <returns>Код ОКАТО из конфигурации.</returns>
        string GetRegion();
    }
}