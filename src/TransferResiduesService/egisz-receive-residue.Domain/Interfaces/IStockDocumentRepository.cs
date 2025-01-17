using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для данных типа <see cref="StockDocument"/> из БД рег. ФРЛЛО.
    /// </summary>
    public interface IStockDocumentRepository
    {
        /// <summary>
        /// Получение даты и времени последней выгрузки остатков в БД рег. ФРЛЛО.
        /// </summary>
        /// <returns>Дата и время последней выгрузки остатков.</returns>
        Task<DateTime> GetLastUnloadingDateAsync();
    }
}