using egisz_receive_residue.Domain.Entities;

namespace egisz_receive_residue.Domain.Services
{
    /// <summary>
    /// Класс для создания сущности <see cref="StockDocument"/>.
    /// </summary>
    public static class AddStockDocumentService
    {
        /// <summary>
        /// Источник загрузки сведений.
        /// </summary>
        private const string SOURCE_VALUE = "DocumentStorage";

        /// <summary>
        /// Создать сущность <see cref="StockDocument"/>.
        /// </summary>
        /// <param name="currentDateTime">Дата и время вставки данных.</param>
        /// <returns>Сущность <see cref="StockDocument"/>.</returns>
        public static StockDocument AddStockDocumentEntity(DateTime currentDateTime)
            => new StockDocument
            {
                DocumentId = Guid.NewGuid().ToString(),
                DocDate = currentDateTime,
                IsUpload = false,
                LastChangeDate = currentDateTime,
                RegNum = null,
                SourceSystem = SOURCE_VALUE,
            };
    }
}
