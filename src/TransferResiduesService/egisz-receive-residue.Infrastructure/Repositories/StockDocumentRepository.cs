using egisz_receive_residue.Domain.Entities;
using egisz_receive_residue.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace egisz_receive_residue.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для данных типа <see cref="StockDocument"/> из БД рег. ФРЛЛО.
    /// </summary>
    /// <remarks>
    /// Конструктор репозитория.
    /// </remarks>
    /// <param name="pharmEgiszContext">Контекст данных БД рег. ФРЛЛО.</param>
    internal class StockDocumentRepository(PharmEgiszContext pharmEgiszContext) : IStockDocumentRepository
    {
        private readonly PharmEgiszContext _pharmEgiszContext = pharmEgiszContext;

        /// <summary>
        /// Получение даты и времени последней выгрузки остатков в БД рег. ФРЛЛО.
        /// </summary>
        /// <returns>Дата и время последней выгрузки остатков. Если в БД рег. ФРЛЛО нет записей, то значение по умолчанию.</returns>
        public async Task<DateTime> GetLastUnloadingDateAsync()
        {
           return await _pharmEgiszContext.Set<StockDocument>()
                .Select(m => m.DocDate)
                .DefaultIfEmpty()
                .MaxAsync();
        }
    }
}